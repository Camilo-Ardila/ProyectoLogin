<template>
  <div class="container">
    <div class="form-box">
      <h2>Authentication Successful!</h2>
      <button @click="logout">Logout</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "AuthSuccessView",
  methods: {
    logout() {
      this.$router.push("/");
    },
  },
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

button {
  width: 100%;
  padding: 12px;
  background-color: #f44336;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.2s;
}

button:hover {
  background-color: #d32f2f;
}
</style>
