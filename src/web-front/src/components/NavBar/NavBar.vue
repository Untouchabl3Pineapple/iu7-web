<template>
  <nav class="navbar-container">
    <div v-if="isInRole == 'guest'" class="navbar-menu-container">
      <GuestNavbarMenu />
    </div>
    <div v-else-if="isInRole == 'admin'" class="navbar-menu-container">
      <AdminNavbarMenu />
    </div>
    <div v-else-if="isInRole == 'foreman'" class="navbar-menu-container">
      <ForemanNavbarMenu />
    </div>
    <div v-else-if="isInRole == 'shop manager'" class="navbar-menu-container">
      <ShopManagerNavbarMenu />
    </div>
    <!-- <div v-else class="navbar-menu-container">
      <GuestNavbarMenu />
      <LoginNavbarMenu />
    </div> -->
  </nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import GuestNavbarMenu from '@/components/NavBar/GuestNavbarMenu.vue'
import AdminNavbarMenu from '@/components/NavBar/AdminNavbarMenu.vue'
import ForemanNavbarMenu from '@/components/NavBar/ForemanNavbarMenu.vue'
import ShopManagerNavbarMenu from '@/components/NavBar/ShopManagerNavbarMenu.vue'


import auth from '@/authentificationService'

export default defineComponent({
  name: "NavBar",
  components: {
    GuestNavbarMenu,
    AdminNavbarMenu,
    ForemanNavbarMenu,
    ShopManagerNavbarMenu
  },
  // data() {

  // },
  computed: {
    isInRole() {
      if (auth.getCurrentUser()) {
        console.log(auth.getCurrentUser().permission);
        return auth.getCurrentUser().permission;
      }
      else {
        auth.logout()
        return "guest"
      }
    }
  },
  methods: {

  }
})
</script>

<style scoped>
.navbar-container {
  margin-bottom: 30px;
}
</style>
