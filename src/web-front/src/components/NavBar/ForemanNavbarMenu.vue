<template>
  <div class="navbar-menu">
    <div class="logo-container">
      <img alt="logo" src="@/assets/logo2.png" class="logo">
    </div>

    <div class="navbar-links" :class="{ 'open': isMobileMenuOpen }">
      <router-link style="text-decoration: none" to="/">
        <NavbarButton>
          Мониторинг
        </NavbarButton>
      </router-link>
      <NavbarButton v-if="isMobile" class="logout-button" @click="logout">
        Выход
      </NavbarButton>
    </div>
    <div v-if="!isMobile" class="logout-container">
      <NavbarButton class="logout-button" @click="logout">
        Выход
      </NavbarButton>
    </div>

    <div class="mobile-menu-button" @click="toggleMobileMenu">
      <div :class="{ 'open': isMobileMenuOpen }" class="hamburger-menu">
        <span></span>
        <span></span>
        <span></span>

      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import NavbarButton from '@/components/Buttons/NavbarButton.vue'
import auth from "@/authentificationService"

export default defineComponent({
  name: "ForemanNavbarMenu",
  components: {
    NavbarButton
  },
  data() {
    return {
      isMobileMenuOpen: false,
      isMobile: window.innerWidth <= 768
    }
  },
  mounted() {
    window.addEventListener('resize', this.checkMobile);
    this.checkMobile(); // вызовите функцию при монтировании компонента для начальной проверки
  },
  beforeUnmount() {
    window.removeEventListener('resize', this.checkMobile);
  },
  methods: {
    checkMobile() {
      this.isMobile = window.innerWidth <= 768;
    },

    logout() {
      auth.logout();
      this.$router.push({ path: '/authorization' })
    },
    toggleMobileMenu() {
      this.isMobileMenuOpen = !this.isMobileMenuOpen;
    }
  }
})
</script>

<style scoped>
.navbar-menu {
  background-color: black;
  display: flex;
  align-items: center;
  /* justify-content: space-between; */
  padding: 10px 0px;
  width: 100%;
}



@media screen and (min-width: 768px) {
  .logout-container {
    margin-left: auto;
    /* Размещаем контейнер с кнопкой выхода вправо */
    margin-right: 10px;
    /* Добавляем отступ справа для разделения от других элементов */
  }
}


.logo-container {
  margin-left: 10px;
  margin-right: 10px;
}

.logo {
  width: 50px;
  height: auto;
}

.navbar-links {
  display: flex;
  align-items: center;

}


/* Styling for NavbarButton component */
.navbar-button {
  margin-right: 15px;
  border: none;
}

.mobile-menu-button {
  display: none;
}

.hamburger-menu {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 30px;
  height: 20px;
  cursor: pointer;
}

.hamburger-menu span {
  background-color: white;
  height: 2px;
  width: 100%;
  transition: all 0.3s ease-in-out;
}

.hamburger-menu span:nth-child(2) {
  opacity: 1;
}

.hamburger-menu span:nth-child(3) {
  transform-origin: bottom;
}

.open span:nth-child(1) {
  transform: translateY(6px) rotate(45deg);
}

.open span:nth-child(2) {
  opacity: 0;
}

.open span:nth-child(3) {
  transform: translateY(-6px) rotate(-45deg);
}

/* Media Query for mobile devices (width less than 768px) */
@media screen and (max-width: 768px) {
  .navbar-menu {
    flex-direction: column;
    align-items: flex-start;
  }

  .navbar-button {
    margin-top: 10px;
  }

  .logo-container {
    margin-bottom: 10px;
  }

  .navbar-links {
    display: none;
    flex-direction: column;
    align-items: flex-start;
    width: 100%;
    padding-left: 20px;
    /* Adjust padding for better visual */
  }

  .navbar-links.open {
    display: flex;
  }

  .mobile-menu-button {
    display: block;
    margin-left: 20px;
    margin-bottom: 10px;
    cursor: pointer;
  }
}
</style>
