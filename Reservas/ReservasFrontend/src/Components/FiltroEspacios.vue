<template>
  <div class="bg-white rounded-xl shadow-md p-6 mb-8">
    <div class="flex items-center gap-3 mb-4">
      <FilterIcon class="w-5 h-5 text-indigo-600" />
      <h2 class="text-xl font-semibold text-gray-800">Filtrar por tipo</h2>
    </div>
    <div class="flex flex-wrap gap-3">
      <button
        v-for="tipo in tipos"
        :key="tipo"
        @click="$emit('cambiar-filtro', tipo)"
        :class="[
          'px-6 py-2 rounded-lg font-medium transition-all',
          filtroActual === tipo
            ? 'bg-indigo-600 text-white shadow-md'
            : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
        ]"
      >
        {{ tipo.charAt(0).toUpperCase() + tipo.slice(1) }}
      </button>
    </div>
  </div>
</template>

<script setup>
import { Filter as FilterIcon } from 'lucide-vue-next';

defineProps({
  filtroActual: {
    type: String,
    required: true
  }
});

defineEmits(['cambiar-filtro']);

const tipos = ['todos', 'aula', 'laboratorio', 'cancha'];
</script>