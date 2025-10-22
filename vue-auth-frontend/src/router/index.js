import { createRouter, createWebHistory } from "vue-router";
import LoginView from "../Views/LoginView.vue";
import RegisterView from "../Views/RegisterView.vue";
import AuthSuccessView from "../Views/AuthSuccessView.vue";
import CanchasView from "../Views/CanchasView.vue";
import AulasView from "../Views/AulasView.vue";
import LaboratoriosView from "../Views/LaboratoriosView.vue";
import ReservasView from "../Views/ReservasView.vue";

const routes = [
  { path: "/", name: "Login", component: LoginView },
  { path: "/register", name: "Register", component: RegisterView },
  { path: "/success", name: "AuthSuccess", component: AuthSuccessView },
  { path: "/spaces/canchas", name: "Canchas", component: CanchasView },
  { path: "/spaces/aulas", name: "Aulas", component: AulasView },
  { path: "/spaces/laboratorios", name: "Laboratorios", component: LaboratoriosView },
  { path: "/reserva/:id", name: "Reservas", component: ReservasView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Navigation guard to protect routes (except login/register)
router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem("sessionToken");
  if (to.path !== "/" && to.path !== "/register" && !isAuthenticated) {
    next("/");
  } else {
    next();
  }
});

export default router;