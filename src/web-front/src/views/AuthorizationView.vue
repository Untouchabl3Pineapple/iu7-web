<template>
    <div class="container">
        <div class="brand">
            <h1 class="brand-name">
                Петербургский<br />тракторный<br />завод
            </h1>
            <img alt="Login" src="@/assets/logo.png" class="logo">
        </div>
        <div class="authorization-wrapper">
            <Authorization />
        </div>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from "vue";
import Authorization from "@/components/Authorization/Authorization.vue"

export default defineComponent({
    name: "AuthorizationView",
    components: {
        Authorization,
    },
})
</script>
  
<style scoped>
.container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: flex-start;
    gap: 0px;
    padding-top: 5%;
    max-width: 100%;
    overflow: hidden;
}

.brand {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 10px;
}

.brand-name {
    font-size: 50px;
    color: black;
    word-break: break-all;
    max-width: 100%;
    overflow: hidden;
    text-align: left;
    line-height: 1.3;
    /* Adjust line height as needed */
}

.logo {
    max-width: 150px;
    /* Set the width of the logo as needed */
    height: auto;
}

.authorization-wrapper {
    width: 100%;
    box-sizing: border-box;
    padding: 0 150px;
    /* Adding padding to prevent content from sticking to the sides */
}

/* Media query for mobile devices (max-width: 767px) */
@media screen and (max-width: 767px) {

    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: flex-start;
        gap: 0px;
        padding-top: 5%;
        max-width: 100%;
        overflow: hidden;
        /* max-width: 90%;   */
        /* Изменение максимальной ширины контейнера */
        padding: 5% 0%;
        /* Изменение отступов контейнера */
    }

    .brand-name {
        font-size: 30px;
        /* Decreased font size for mobile */
    }

    .logo {
        max-width: 100px;
        /* Decreased width for mobile */
    }

    .authorization-wrapper {
        padding: 0 20px;
        /* Decreased padding for mobile */
    }
}
</style>
  