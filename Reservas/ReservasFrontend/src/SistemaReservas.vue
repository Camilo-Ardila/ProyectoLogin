<template>
  <div class="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 p-4 md:p-8">
    <!-- Notificación -->
    <transition name="fade">
      <div 
        v-if="notificacion"
        :class="[
          'fixed top-4 right-4 z-50 px-6 py-4 rounded-lg shadow-lg flex items-center gap-3',
          notificacion.tipo === 'success' ? 'bg-green-500' : 'bg-red-500',
          'text-white'
        ]"
      >
        <component :is="notificacion.tipo === 'success' ? 'CheckIcon' : 'XIcon'" class="w-5 h-5" />
        <span>{{ notificacion.mensaje }}</span>
      </div>
    </transition>

    <div class="max-w-7xl mx-auto">
      <!-- Header -->
      <header class="text-center mb-12">
        <h1 class="text-4xl md:text-5xl font-bold text-gray-800 mb-3">
          Sistema de Reservas Universitarias
        </h1>
        <p class="text-gray-600 text-lg">
          Reserva tus espacios de forma rápida y sencilla
        </p>
      </header>

      <!-- Filtros -->
      <div class="bg-white rounded-xl shadow-md p-6 mb-8">
        <div class="flex items-center gap-3 mb-4">
          <FilterIcon class="w-5 h-5 text-indigo-600" />
          <h2 class="text-xl font-semibold text-gray-800">Filtrar por tipo</h2>
        </div>
        <div class="flex flex-wrap gap-3">
          <button
            v-for="tipo in ['todos', 'aula', 'laboratorio', 'cancha']"
            :key="tipo"
            @click="filtroTipo = tipo"
            :class="[
              'px-6 py-2 rounded-lg font-medium transition-all',
              filtroTipo === tipo
                ? 'bg-indigo-600 text-white shadow-md'
                : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
            ]"
          >
            {{ tipo.charAt(0).toUpperCase() + tipo.slice(1) }}
          </button>
        </div>
      </div>

      <!-- Lista de Espacios -->
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-12">
        <div 
          v-for="espacio in espaciosFiltrados"
          :key="espacio.id"
          class="bg-white rounded-xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow"
        >
          <img 
            :src="espacio.imagen" 
            :alt="espacio.nombre"
            class="w-full h-48 object-cover"
          />
          <div class="p-6">
            <div class="flex items-center justify-between mb-3">
              <h3 class="text-xl font-bold text-gray-800">{{ espacio.nombre }}</h3>
              <span class="px-3 py-1 bg-indigo-100 text-indigo-700 rounded-full text-sm font-medium">
                {{ espacio.tipo }}
              </span>
            </div>
            <p class="text-gray-600 mb-4 text-sm">{{ espacio.descripcion }}</p>
            <div class="flex items-center gap-4 mb-4 text-gray-600">
              <div class="flex items-center gap-2">
                <UsersIcon class="w-4 h-4" />
                <span class="text-sm">{{ espacio.capacidad }} personas</span>
              </div>
            </div>
            <button
              @click="abrirFormulario(espacio)"
              class="w-full bg-indigo-600 hover:bg-indigo-700 text-white py-3 rounded-lg font-medium transition-colors"
            >
              Reservar Espacio
            </button>
          </div>
        </div>
      </div>

      <!-- Mis Reservas -->
      <div v-if="reservas.length > 0" class="bg-white rounded-xl shadow-lg p-6">
        <h2 class="text-2xl font-bold text-gray-800 mb-6">Mis Reservas</h2>
        <div class="space-y-4">
          <div 
            v-for="reserva in reservas"
            :key="reserva.id"
            class="flex items-center justify-between p-4 bg-gray-50 rounded-lg border border-gray-200"
          >
            <div class="flex-1">
              <h3 class="font-semibold text-gray-800 mb-2">{{ reserva.espacioNombre }}</h3>
              <div class="flex flex-wrap gap-4 text-sm text-gray-600">
                <div class="flex items-center gap-2">
                  <CalendarIcon class="w-4 h-4" />
                  <span>{{ reserva.fecha }}</span>
                </div>
                <div class="flex items-center gap-2">
                  <ClockIcon class="w-4 h-4" />
                  <span>{{ reserva.horaInicio }} - {{ reserva.horaFin }}</span>
                </div>
                <div class="flex items-center gap-2">
                  <UsersIcon class="w-4 h-4" />
                  <span>{{ reserva.nombre }}</span>
                </div>
              </div>
            </div>
            <button
              @click="cancelarReserva(reserva.id)"
              class="ml-4 px-4 py-2 bg-red-500 hover:bg-red-600 text-white rounded-lg font-medium transition-colors"
            >
              Cancelar
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal de Formulario -->
    <transition name="modal">
      <div 
        v-if="mostrarFormulario && espacioSeleccionado"
        class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-40 p-4"
        @click.self="cerrarFormulario"
      >
        <div class="bg-white rounded-xl shadow-2xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
          <!-- Imagen del espacio -->
          <img 
            :src="espacioSeleccionado.imagen" 
            :alt="espacioSeleccionado.nombre"
            class="w-full h-64 object-cover"
          />
          
          <div class="p-8">
            <div class="flex items-center justify-between mb-6">
              <h2 class="text-3xl font-bold text-gray-800">
                Reservar {{ espacioSeleccionado.nombre }}
              </h2>
              <button
                @click="cerrarFormulario"
                class="p-2 hover:bg-gray-100 rounded-full transition-colors"
              >
                <XIcon class="w-6 h-6" />
              </button>
            </div>

            <div class="space-y-6">
              <div>
                <label class="block text-gray-700 font-medium mb-2">
                  Nombre Completo *
                </label>
                <input
                  v-model="formData.nombre"
                  type="text"
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                  placeholder="Ingresa tu nombre"
                />
              </div>

              <div>
                <label class="block text-gray-700 font-medium mb-2">
                  Fecha *
                </label>
                <input
                  v-model="formData.fecha"
                  type="date"
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                />
              </div>

              <div class="grid grid-cols-2 gap-4">
                <div>
                  <label class="block text-gray-700 font-medium mb-2">
                    Hora Inicio *
                  </label>
                  <input
                    v-model="formData.horaInicio"
                    type="time"
                    class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                  />
                </div>
                <div>
                  <label class="block text-gray-700 font-medium mb-2">
                    Hora Fin *
                  </label>
                  <input
                    v-model="formData.horaFin"
                    type="time"
                    class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                  />
                </div>
              </div>

              <div class="flex gap-4 pt-4">
                <button
                  @click="cerrarFormulario"
                  class="flex-1 px-6 py-3 bg-gray-200 hover:bg-gray-300 text-gray-800 rounded-lg font-medium transition-colors"
                >
                  Cancelar
                </button>
                <button
                  @click="crearReserva"
                  class="flex-1 px-6 py-3 bg-indigo-600 hover:bg-indigo-700 text-white rounded-lg font-medium transition-colors"
                >
                  Confirmar Reserva
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue';
import { Calendar as CalendarIcon, Clock as ClockIcon, Users as UsersIcon, X as XIcon, Check as CheckIcon, Filter as FilterIcon } from 'lucide-vue-next';

// Datos de espacios disponibles
const espacios = ref([
  {
    id: 1,
    nombre: 'Aula 101',
    tipo: 'aula',
    capacidad: 30,
    imagen: 'https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=400',
    descripcion: 'Aula equipada con proyector y pizarra inteligente'
  },
  {
    id: 2,
    nombre: 'Laboratorio de Computación',
    tipo: 'laboratorio',
    capacidad: 25,
    imagen: 'https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=400',
    descripcion: '25 computadores de última generación'
  },
  {
    id: 3,
    nombre: 'Cancha de Fútbol',
    tipo: 'cancha',
    capacidad: 22,
    imagen: 'https://images.unsplash.com/photo-1529900748604-07564a03e7a6?w=400',
    descripcion: 'Cancha sintética con iluminación nocturna'
  },
  {
    id: 4,
    nombre: 'Aula 205',
    tipo: 'aula',
    capacidad: 40,
    imagen: 'https://images.unsplash.com/photo-1562774053-701939374585?w=400',
    descripcion: 'Aula amplia con sistema de audio'
  },
  {
    id: 5,
    nombre: 'Laboratorio de Química',
    tipo: 'laboratorio',
    capacidad: 20,
    imagen: 'https://images.unsplash.com/photo-1532094349884-543bc11b234d?w=400',
    descripcion: 'Equipado con campanas extractoras y material de seguridad'
  },
  {
    id: 6,
    nombre: 'Cancha de Baloncesto',
    tipo: 'cancha',
    capacidad: 10,
    imagen: 'https://images.unsplash.com/photo-1546519638-68e109498ffc?w=400',
    descripcion: 'Cancha cubierta profesional'
  }
]);

const reservas = ref([]);
const filtroTipo = ref('todos');
const mostrarFormulario = ref(false);
const espacioSeleccionado = ref(null);
const notificacion = ref(null);

const formData = ref({
  nombre: '',
  fecha: '',
  horaInicio: '',
  horaFin: ''
});

// Cargar reservas del localStorage
onMounted(() => {
  const reservasGuardadas = localStorage.getItem('reservas');
  if (reservasGuardadas) {
    reservas.value = JSON.parse(reservasGuardadas);
  }
});

// Guardar reservas en localStorage cuando cambien
watch(reservas, (newReservas) => {
  localStorage.setItem('reservas', JSON.stringify(newReservas));
}, { deep: true });

// Computed: Espacios filtrados
const espaciosFiltrados = computed(() => {
  if (filtroTipo.value === 'todos') {
    return espacios.value;
  }
  return espacios.value.filter(e => e.tipo === filtroTipo.value);
});

// Función para mostrar notificación
const mostrarNotificacion = (mensaje, tipo = 'success') => {
  notificacion.value = { mensaje, tipo };
  setTimeout(() => {
    notificacion.value = null;
  }, 3000);
};

// Validar fecha no sea pasada
const validarFecha = (fecha) => {
  const hoy = new Date();
  hoy.setHours(0, 0, 0, 0);
  const fechaSeleccionada = new Date(fecha + 'T00:00:00');
  return fechaSeleccionada >= hoy;
};

// Verificar si existe reserva duplicada
const existeReservaDuplicada = (espacioId, fecha, horaInicio) => {
  return reservas.value.some(r => 
    r.espacioId === espacioId && 
    r.fecha === fecha && 
    r.horaInicio === horaInicio
  );
};

// Abrir formulario de reserva
const abrirFormulario = (espacio) => {
  espacioSeleccionado.value = espacio;
  mostrarFormulario.value = true;
  formData.value = { nombre: '', fecha: '', horaInicio: '', horaFin: '' };
};

// Cerrar formulario
const cerrarFormulario = () => {
  mostrarFormulario.value = false;
  espacioSeleccionado.value = null;
  formData.value = { nombre: '', fecha: '', horaInicio: '', horaFin: '' };
};

// Crear reserva
const crearReserva = () => {
  // Validaciones
  if (!formData.value.nombre.trim()) {
    mostrarNotificacion('Por favor ingrese su nombre', 'error');
    return;
  }

  if (!formData.value.fecha) {
    mostrarNotificacion('Por favor seleccione una fecha', 'error');
    return;
  }

  if (!formData.value.horaInicio || !formData.value.horaFin) {
    mostrarNotificacion('Por favor seleccione las horas de inicio y fin', 'error');
    return;
  }

  if (!validarFecha(formData.value.fecha)) {
    mostrarNotificacion('No se pueden hacer reservas en fechas pasadas', 'error');
    return;
  }

  if (formData.value.horaInicio >= formData.value.horaFin) {
    mostrarNotificacion('La hora de fin debe ser posterior a la hora de inicio', 'error');
    return;
  }

  // Verificar duplicados
  if (existeReservaDuplicada(espacioSeleccionado.value.id, formData.value.fecha, formData.value.horaInicio)) {
    mostrarNotificacion('Ya existe una reserva para este espacio en el horario seleccionado', 'error');
    return;
  }

  // Crear nueva reserva
  const nuevaReserva = {
    id: Date.now(),
    espacioId: espacioSeleccionado.value.id,
    espacioNombre: espacioSeleccionado.value.nombre,
    ...formData.value
  };

  reservas.value.push(nuevaReserva);
  mostrarNotificacion('¡Reserva creada exitosamente!', 'success');
  cerrarFormulario();
};

// Cancelar reserva
const cancelarReserva = (id) => {
  reservas.value = reservas.value.filter(r => r.id !== id);
  mostrarNotificacion('Reserva cancelada correctamente', 'success');
};
</script>

<style scoped>
.fade-enter-active, .fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
}

.modal-enter-active, .modal-leave-active {
  transition: opacity 0.3s;
}
.modal-enter-from, .modal-leave-to {
  opacity: 0;
}
</style>