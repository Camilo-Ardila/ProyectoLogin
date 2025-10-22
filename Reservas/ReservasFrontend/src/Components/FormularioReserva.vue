<template>
  <transition name="modal">
    <div 
      v-if="visible"
      class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-40 p-4"
      @click.self="cerrar"
    >
      <div class="bg-white rounded-xl shadow-2xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
        <!-- Imagen del espacio -->
        <img 
          :src="espacio.imagen" 
          :alt="espacio.nombre"
          class="w-full h-64 object-cover"
        />
        
        <div class="p-8">
          <div class="flex items-center justify-between mb-6">
            <h2 class="text-3xl font-bold text-gray-800">
              Reservar {{ espacio.nombre }}
            </h2>
            <button
              @click="cerrar"
              class="p-2 hover:bg-gray-100 rounded-full transition-colors"
            >
              <XIcon class="w-6 h-6" />
            </button>
          </div>

          <div class="space-y-6">
            <!-- Nombre -->
            <div>
              <label class="block text-gray-700 font-medium mb-2">
                Nombre Completo *
              </label>
              <input
                v-model="form.nombre"
                type="text"
                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                placeholder="Ingresa tu nombre"
              />
            </div>

            <!-- Fecha -->
            <div>
              <label class="block text-gray-700 font-medium mb-2">
                Fecha *
              </label>
              <input
                v-model="form.fecha"
                type="date"
                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
              />
            </div>

            <!-- Horas -->
            <div class="grid grid-cols-2 gap-4">
              <div>
                <label class="block text-gray-700 font-medium mb-2">
                  Hora Inicio *
                </label>
                <input
                  v-model="form.horaInicio"
                  type="time"
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                />
              </div>
              <div>
                <label class="block text-gray-700 font-medium mb-2">
                  Hora Fin *
                </label>
                <input
                  v-model="form.horaFin"
                  type="time"
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                />
              </div>
            </div>

            <!-- Botones -->
            <div class="flex gap-4 pt-4">
              <button
                @click="cerrar"
                type="button"
                class="flex-1 px-6 py-3 bg-gray-200 hover:bg-gray-300 text-gray-800 rounded-lg font-medium transition-colors"
              >
                Cancelar
              </button>
              <button
                @click="confirmarReserva"
                type="button"
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
</template>

<script setup>
import { ref, watch } from 'vue';
import { X as XIcon } from 'lucide-vue-next';

const props = defineProps({
  visible: Boolean,
  espacio: {
    type: Object,
    required: true
  }
});

const emit = defineEmits(['cerrar', 'confirmar']);

const form = ref({
  nombre: '',
  fecha: '',
  horaInicio: '',
  horaFin: ''
});

// Limpiar formulario cuando se cierra
watch(() => props.visible, (nuevo) => {
  if (!nuevo) {
    form.value = {
      nombre: '',
      fecha: '',
      horaInicio: '',
      horaFin: ''
    };
  }
});

const cerrar = () => {
  emit('cerrar');
};

const confirmarReserva = () => {
  emit('confirmar', {
    ...form.value,
    espacioId: props.espacio.id,
    espacioNombre: props.espacio.nombre
  });
};
</script>

<style scoped>
.modal-enter-active, .modal-leave-active {
  transition: opacity 0.3s;
}
.modal-enter-from, .modal-leave-to {
  opacity: 0;
}
</style>