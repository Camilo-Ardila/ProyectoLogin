import { createRouter, createWebHistory } from "vue-router";
import LoginView from "../Views/LoginView.vue";
import RegisterView from "../Views/RegisterView.vue";
import AuthSuccessView from "../Views/AuthSuccessView.vue";
import SpacesView from "../Views/SpacesView.vue"; // Updated to match your file naming

const routes = [
  { path: "/", name: "Login", component: LoginView },
  { path: "/register", name: "Register", component: RegisterView },
  { path: "/success", name: "AuthSuccess", component: AuthSuccessView },
  { path: "/spaces", name: "Spaces", component: SpacesView }, // Added for RF1
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Navigation guard to protect routes (except login/register)
router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem('sessionToken');
  if (to.path !== '/' && to.path !== '/register' && !isAuthenticated) {
    next('/');
  } else {
    next();
  }
});

export default router;