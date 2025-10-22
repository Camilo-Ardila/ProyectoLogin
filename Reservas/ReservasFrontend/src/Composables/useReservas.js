import { ref, watch } from 'vue';

export function useReservas() {
  const reservas = ref([]);
  const notificacion = ref(null);

  // Cargar reservas del localStorage al iniciar
  const cargarReservas = () => {
    const guardadas = localStorage.getItem('reservas');
    if (guardadas) {
      reservas.value = JSON.parse(guardadas);
    }
  };

  // Guardar en localStorage cuando cambien las reservas
  watch(reservas, (nuevas) => {
    localStorage.setItem('reservas', JSON.stringify(nuevas));
  }, { deep: true });

  // Mostrar notificación temporal
  const mostrarNotificacion = (mensaje, tipo = 'success') => {
    notificacion.value = { mensaje, tipo };
    
    // Reproducir sonido de éxito
    if (tipo === 'success') {
      reproducirSonidoExito();
    }
    
    setTimeout(() => {
      notificacion.value = null;
    }, 3000);
  };

  // Reproducir sonido de confirmación
  const reproducirSonidoExito = () => {
    // Crear un contexto de audio
    const audioContext = new (window.AudioContext || window.webkitAudioContext)();
    
    // Crear oscilador para el sonido
    const oscillator = audioContext.createOscillator();
    const gainNode = audioContext.createGain();
    
    oscillator.connect(gainNode);
    gainNode.connect(audioContext.destination);
    
    // Configurar el sonido (dos tonos: do-mi)
    oscillator.frequency.setValueAtTime(523.25, audioContext.currentTime); // Do (C5)
    oscillator.frequency.setValueAtTime(659.25, audioContext.currentTime + 0.1); // Mi (E5)
    
    // Configurar volumen con fade out
    gainNode.gain.setValueAtTime(0.3, audioContext.currentTime);
    gainNode.gain.exponentialRampToValueAtTime(0.01, audioContext.currentTime + 0.3);
    
    // Reproducir
    oscillator.start(audioContext.currentTime);
    oscillator.stop(audioContext.currentTime + 0.3);
  };

  // Validar que la fecha no sea pasada
  const validarFecha = (fecha) => {
    const hoy = new Date();
    hoy.setHours(0, 0, 0, 0);
    const fechaSeleccionada = new Date(fecha + 'T00:00:00');
    return fechaSeleccionada >= hoy;
  };

  // Verificar si hay conflicto de horarios (solapamiento)
  const hayConflictoHorario = (espacioId, fecha, horaInicio, horaFin) => {
    // Buscar reservas del mismo espacio en la misma fecha
    const reservasMismoEspacioYFecha = reservas.value.filter(r => 
      r.espacioId === espacioId && r.fecha === fecha
    );

    // Verificar si hay solapamiento de horarios
    for (const reserva of reservasMismoEspacioYFecha) {
      const reservaInicio = reserva.horaInicio;
      const reservaFin = reserva.horaFin;

      // Casos de solapamiento:
      // 1. La nueva reserva empieza durante una reserva existente
      // 2. La nueva reserva termina durante una reserva existente
      // 3. La nueva reserva contiene completamente a una reserva existente
      const hayConflicto = (
        (horaInicio >= reservaInicio && horaInicio < reservaFin) ||  // Empieza durante
        (horaFin > reservaInicio && horaFin <= reservaFin) ||        // Termina durante
        (horaInicio <= reservaInicio && horaFin >= reservaFin)       // Contiene completamente
      );

      if (hayConflicto) {
        return true;
      }
    }

    return false;
  };

  // Crear nueva reserva
  const crearReserva = (datos) => {
    // Validaciones básicas
    if (!datos.nombre || !datos.nombre.trim()) {
      mostrarNotificacion('Por favor ingrese su nombre', 'error');
      return false;
    }

    if (!datos.fecha) {
      mostrarNotificacion('Por favor seleccione una fecha', 'error');
      return false;
    }

    if (!datos.horaInicio || !datos.horaFin) {
      mostrarNotificacion('Por favor seleccione las horas', 'error');
      return false;
    }

    if (!validarFecha(datos.fecha)) {
      mostrarNotificacion('No se pueden hacer reservas en fechas pasadas', 'error');
      return false;
    }

    if (datos.horaInicio >= datos.horaFin) {
      mostrarNotificacion('La hora de fin debe ser posterior a la de inicio', 'error');
      return false;
    }

    // Verificar conflictos de horario
    if (hayConflictoHorario(datos.espacioId, datos.fecha, datos.horaInicio, datos.horaFin)) {
      mostrarNotificacion('El espacio ya está reservado en ese horario', 'error');
      return false;
    }

    // Crear reserva
    const nuevaReserva = {
      id: Date.now(),
      ...datos
    };

    reservas.value.push(nuevaReserva);
    mostrarNotificacion('¡Reserva creada exitosamente!', 'success');
    return true;
  };

  // Cancelar reserva
  const cancelarReserva = (id) => {
    reservas.value = reservas.value.filter(r => r.id !== id);
    mostrarNotificacion('Reserva cancelada correctamente', 'success');
  };

  return {
    reservas,
    notificacion,
    cargarReservas,
    mostrarNotificacion,
    crearReserva,
    cancelarReserva
  };
}