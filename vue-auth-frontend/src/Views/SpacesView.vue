<template>
  <div class="spaces-container">
    <h2>Available Spaces</h2>
    <ul>
      <li v-for="space in spaces" :key="space.id">{{ space.name }}</li>
    </ul>
    <button @click="logout">Logout</button>
  </div>
</template>

<script>
export default {
  name: "SpacesView",
  data() {
    return {
      spaces: JSON.parse(localStorage.getItem('spaces') || '[]')
    };
  },
  methods: {
    logout() {
      localStorage.removeItem('sessionToken');
      this.$router.push('/');
    }
  },
  mounted() {
    if (!this.spaces.length) {
      const spaces = [
        { id: 1, name: 'Aula 101' },
        { id: 2, name: 'Cancha Deportiva' },
        { id: 3, name: 'Laboratorio 1' }
      ];
      localStorage.setItem('spaces', JSON.stringify(spaces));
      this.spaces = spaces;
    }
  }
};
</script>

<style scoped>
.spaces-container {
  padding: 20px;
  max-width: 600px;
  margin: 0 auto;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  padding: 10px;
  border-bottom: 1px solid #ccc;
}
button {
  padding: 10px 20px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
button:hover {
  background-color: #45a049;
}
</style>