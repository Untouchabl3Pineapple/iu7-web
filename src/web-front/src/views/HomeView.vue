<template>
  <div class="home">
    <NavBar></NavBar>
    <MonitoringBoard></MonitoringBoard>
    <EventsTable></EventsTable>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import NavBar from "@/components/NavBar/NavBar.vue"
import EventsTable from "@/components/Tables/EventsTable.vue"
import MonitoringBoard from "@/components/MonitoringBoard/MonitoringBoard.vue"


export default defineComponent({
  name: 'HomeView',
  components: {
    NavBar,
    EventsTable,
    MonitoringBoard
    // HelloWorld,
  },
});
</script>


<style scoped></style>