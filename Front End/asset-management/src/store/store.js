import { createStore } from "vuex";
import createPersistedState from "vuex-persistedstate";

// Create a new store instance.
const store = createStore({
  state() {
    return {
      user: null,
    };
  },

  getters: {
    user: (state) => state.user,
  },

  mutations: {
    setUser(state, user) {
      state.user = user;
    },
  },

  actions: {
    increment(context) {
      context.commit("setUser");
    },
  },
  plugins: [createPersistedState()],
});

export default store;
