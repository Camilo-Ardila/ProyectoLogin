<template>
  <div class="bg-white rounded-xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow">
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
        @click="$emit('reservar', espacio)"
        class="w-full bg-indigo-600 hover:bg-indigo-700 text-white py-3 rounded-lg font-medium transition-colors"
      >
        Reservar Espacio
      </button>
    </div>
  </div>
</template>

<script setup>
import { Users as UsersIcon } from 'lucide-vue-next';

defineProps({
  espacio: {
    type: Object,
    required: true
  }
});

defineEmits(['reservar']);
</script>