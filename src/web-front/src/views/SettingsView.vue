<template>
    <div>
        <NavBar> </NavBar>
        <UsersRedactor></UsersRedactor>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';

import NavBar from "@/components/NavBar/NavBar.vue"
import UsersRedactor from '@/components/Redactor/UsersRedactor.vue';


export default defineComponent({
    name: 'SettingsView',
    components: {
        NavBar,
        UsersRedactor,
    },
});
</script>
  
  
<style scoped></style>