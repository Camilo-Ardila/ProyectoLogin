<template>
  <div class="container">
    <div class="form-box">
      <h2>Reservar espacios</h2>
      <router-link to="/spaces/canchas" class="button">Canchas</router-link>
      <router-link to="/spaces/aulas" class="button">Aulas</router-link>
      <router-link to="/spaces/laboratorios" class="button">Laboratorios</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "AuthSuccessView",
};
</script>

<style scoped>
.container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: #f0f2f5;
}

.form-box {
  background-color: #ffffff;
  padding: 40px;
  border-radius: 10px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
  width: 350px;
  text-align: center;
}

h2 {
  margin-bottom: 25px;
  color: #333333;
}

.button {
  display: block;
  width: 100%;
  padding: 12px;
  margin: 10px 0;
  background-color: #4caf50;
  color: white;
  text-decoration: none;
  border: none;
  border-radius: 5px;
  font-weight: bold;
  text-align: center;
  transition: background-color 0.2s;
}

.button:hover {
  background-color: #45a049;
}
</style>