<template>
    <div>
        <NavBar> </NavBar>
        <EventsRedactor></EventsRedactor>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';

import NavBar from "@/components/NavBar/NavBar.vue"
import EventsRedactor from "@/components/Redactor/EventsRedactor.vue"


export default defineComponent({
    name: 'EventsRedactorView',
    components: {
        NavBar,
        EventsRedactor,
    },
});
</script>
  
  
<style scoped></style>