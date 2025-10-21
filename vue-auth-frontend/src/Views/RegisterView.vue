<template>
  <div class="container">
    <div class="form-box">
      <h2>Sign Up</h2>
      <form @submit.prevent="handleRegister">
        <input v-model="nombre" placeholder="Nombre" required />
        <input v-model="username" placeholder="Username" required />
        <input v-model="email" placeholder="Email" type="email" required />
        <input type="password" v-model="password" placeholder="Password" required />
        <button type="submit" :disabled="loading">Register</button>
      </form>
      <p v-if="error" class="error">{{ error }}</p>
      <p>
        Already have an account?
        <router-link to="/">Login</router-link>
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterView",
  data() {
    return {
      nombre: "",
      username: "",
      email: "",
      password: "",
      error: "",
      loading: false,
    };
  },
  methods: {
    async handleRegister() {
      this.error = "";
      this.loading = true;
      try {
        const response = await fetch("http://localhost:5000/api/users/register", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            nombre: this.nombre,
            username: this.username,
            email: this.email,
            password: this.password,
          }),
        });

        const data = await response.json();

        if (response.ok) {
          alert("Registration successful! Please log in.");
          this.$router.push("/");
        } else {
          this.error = data.message || "Registration failed";
        }
      } catch (error) {
        this.error = "An error occurred. Please try again later.";
      } finally {
        this.loading = false;
      }
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

input {
  width: 100%;
  padding: 12px 15px;
  margin: 10px 0;
  border-radius: 5px;
  border: 1px solid #ccc;
  outline: none;
  transition: all 0.2s;
}

input:focus {
  border-color: #4caf50;
  box-shadow: 0 0 5px rgba(76, 175, 80, 0.5);
}

button {
  width: 100%;
  padding: 12px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 5px;
  margin-top: 15px;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.2s;
}

button:hover {
  background-color: #45a049;
}

button:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}

.error {
  color: red;
  margin-top: 10px;
}
</style>