<template>
  <div v-if="reservas.length > 0" class="bg-white rounded-xl shadow-lg p-6">
    <h2 class="text-2xl font-bold text-gray-800 mb-6">Mis Reservas</h2>
    <div class="space-y-4">
      <div 
        v-for="reserva in reservas"
        :key="reserva.id"
        class="flex flex-col md:flex-row md:items-center justify-between p-4 bg-gray-50 rounded-lg border border-gray-200 gap-4"
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
          @click="$emit('cancelar', reserva.id)"
          class="px-4 py-2 bg-red-500 hover:bg-red-600 text-white rounded-lg font-medium transition-colors"
        >
          Cancelar
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { Calendar as CalendarIcon, Clock as ClockIcon, Users as UsersIcon } from 'lucide-vue-next';

defineProps({
  reservas: {
    type: Array,
    required: true
  }
});

defineEmits(['cancelar']);
</script>