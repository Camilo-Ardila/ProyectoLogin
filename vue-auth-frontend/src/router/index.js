import { createRouter, createWebHistory } from "vue-router";
import LoginView from "../Views/LoginView.vue";
import RegisterView from "../Views/RegisterView.vue";
import AuthSuccessView from "../Views/AuthSuccessView.vue";

const routes = [
  { path: "/", name: "Login", component: LoginView },
  { path: "/register", name: "Register", component: RegisterView },
  { path: "/success", name: "AuthSuccess", component: AuthSuccessView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
