<template>
    <div>
        <NavBar> </NavBar>
        <EventsTypesRedactor></EventsTypesRedactor>

        <!-- <EventsDetail></EventsDetail> -->
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';

import EventsTypesRedactor from "@/components/Redactor/EventsTypesRedactor.vue"
import NavBar from "@/components/NavBar/NavBar.vue"


export default defineComponent({
    name: 'EventsTypesRedactorView',
    components: {
        // EventsDetail
        NavBar,
        EventsTypesRedactor,
    },
});
</script>
  
  
<style scoped></style>