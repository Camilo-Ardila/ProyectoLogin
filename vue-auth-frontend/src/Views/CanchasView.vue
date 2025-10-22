<template>
  <div class="container">
    <h2>Canchas</h2>
    <div v-if="loading" class="loading">Loading...</div>
    <div v-else-if="error" class="error">{{ error }}</div>
    <div v-else class="space-list">
      <div v-for="space in spaces" :key="space.id" class="space-item">
        <p><strong>Name:</strong> {{ space.name || 'N/A' }}</p>
        <p><strong>Status:</strong> {{ space.status || 'N/A' }}</p>
        <router-link :to="`/reserva/${space.id}`" class="button">Ver Disponibilidad</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import api from "../api";

export default {
  name: "CanchasView",
  data() {
    return {
      spaces: [],
      loading: true,
      error: null,
    };
  },
  mounted() {
    this.fetchCanchas();
  },
  methods: {
    async fetchCanchas() {
      try {
        const response = await api.get("/canchas");
        console.log("Fetched data:", response.data); // Debug log
        this.spaces = response.data;
      } catch (err) {
        this.error = "Failed to load canchas. Please try again.";
        console.error("Error fetching canchas:", err);
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>

<style scoped>
.container {
  padding: 20px;
  max-width: 800px;
  margin: 0 auto;
}

h2 {
  text-align: center;
  color: #333;
}

.loading {
  text-align: center;
  color: #666;
}

.error {
  text-align: center;
  color: red;
}

.space-list {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.space-item {
  background: #fff;
  padding: 15px;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 300px;
}

.button {
  display: inline-block;
  padding: 8px 16px;
  background-color: #4caf50;
  color: white;
  text-decoration: none;
  border-radius: 5px;
  font-weight: bold;
  transition: background-color 0.2s;
}

.button:hover {
  background-color: #45a049;
}
</style>