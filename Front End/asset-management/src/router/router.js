import { createRouter, createWebHistory } from "vue-router";
import Home from "../components/layout/BaseHome.vue";
import Login from "../components/layout/TheLogin.vue";
import store from "../store/store";
import HUSTContent from "../components/layout/TheContent.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: { requiresAuth: true },
    children: [
      { path: "/asset", component: HUSTContent },
    ],
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// const isAuthenticated = true;

/**
 * Mô tả : Authentic
 * @param
 * @return
 * Created by: Lê Thiện Tuấn - MF1118`
 * Created date: 21:06 01/06/2022
 */
router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth) {
    if (store.getters.user) {
      next();
      return;
    }
    next("/login");
  } else {
    next();
  }
});

export default router;
