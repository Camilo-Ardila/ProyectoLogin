import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5000/api", // your backend
  headers: { "Content-Type": "application/json" },
});

export const registerUser = (user) => api.post("/users/register", user);
export const loginUser = (user) => api.post("/users/login", user);

export default api;
