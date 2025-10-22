<template>
  <div class="container">
    <h2>Ver Disponibilidad y Reservar</h2>
    <div v-if="loading" class="loading">Loading...</div>
    <div v-else-if="error" class="error">{{ error }}</div>
    <div v-else class="reservation-form">
      <p><strong>Espacio:</strong> {{ space.name || 'N/A' }}</p>
      <p><strong>Estado:</strong> {{ space.status || 'N/A' }}</p>
      <label for="reservationDate">Selecciona una fecha:</label>
      <input type="date" v-model="selectedDate" id="reservationDate" :min="minDate" class="date-input" />
      <button @click="makeReservation" class="button" :disabled="!selectedDate">Reservar</button>
    </div>
  </div>
</template>

<script>
import api from "../api";

export default {
  name: "ReservasView",
  data() {
    return {
      spaceId: this.$route.params.id,
      space: {},
      selectedDate: null,
      minDate: new Date().toISOString().split("T")[0],
      loading: true,
      error: null,
    };
  },
  mounted() {
    this.fetchSpace();
  },
  methods: {
    async fetchSpace() {
      try {
        console.log("Fetching space with id:", this.$route.params.id); // Debug route param
        const response = await api.get(`/reservas/${this.spaceId}`);
        this.space = response.data;
      } catch (err) {
        this.error = "Failed to load space details. Please try again.";
        console.error("Error fetching space:", err);
      } finally {
        this.loading = false;
      }
    },
    async makeReservation() {
      if (!this.selectedDate) return;
      const diaReserva = new Date(this.selectedDate); // Parse the date string
      console.log("Making reservation for spaceId:", this.spaceId); // Debug spaceId
      try {
        const response = await api.post("/reservas/reservar", {
          idUsuario: 1, // Replace with logged-in user ID
          idEspacio: this.spaceId, // Ensure correct id is sent
          diaReserva: diaReserva.toISOString().split("T")[0], // Ensure yyyy-MM-dd format
        });
        this.error = null;
        alert(response.data.message); // Show success message
      } catch (err) {
        this.error = err.response?.data?.message || "Reservation failed. Please try again.";
        console.error("Error making reservation:", err);
      }
    },
  },
};
</script>

<style scoped>
.container {
  padding: 20px;
  max-width: 600px;
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

.reservation-form {
  background: #fff;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.date-input {
  display: block;
  width: 100%;
  padding: 8px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.button {
  display: inline-block;
  padding: 10px 20px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 5px;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.2s;
}

.button:hover:not(:disabled) {
  background-color: #45a049;
}

.button:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}
</style>