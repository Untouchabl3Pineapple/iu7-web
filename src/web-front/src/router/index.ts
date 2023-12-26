import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeView from "@/views/HomeView.vue"
import AuthorizationView from "@/views/AuthorizationView.vue"
import EventsRedactorView from "@/views/EventsRedactorView.vue"
import EventsTypesRedactorView from "@/views/EventsTypesRedactorView.vue"
import SettingsView from "@/views/SettingsView.vue"


const routes: Array<RouteRecordRaw> = [
  {
    path: "/authorization",
    name: "authorization",
    component: AuthorizationView,
  },
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  { 
    path: '/events/:id', 
    name: "eventsRedactor",
    props: true,
    component: EventsRedactorView, 
  },
  {
    path: "/types",
    name: "eventsTypesRedactor",
    component: EventsTypesRedactorView,
  },
  {
    path: "/settings",
    name: "settings",
    component: SettingsView,
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
