<template>
    <div>
        <InputEventType v-if="isInput" @input="handleDataFromInputEventType" @add-event-clicked="addEventType"
            @delete-event-clicked="setInput(false)">
        </InputEventType>
        <AddButton v-if="rowId < 0 && !isInput" @click="setInput(true)">Добавить тип происшествия</AddButton>
        <DeleteButton v-if="rowId > 0 && !isInput" @click="deleteEventType(rowId)">Удалить тип происшествия</DeleteButton>
        <EventsTypesTable @row-selected="getRowId"></EventsTypesTable>

    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';

import EventsTypesTable from "@/components/Tables/EventsTypesTable.vue"
import AddButton from "@/components/Buttons/AddButton.vue"
import DeleteButton from "@/components/Buttons/DeleteButton.vue"
import InputEventType from "@/components/Input/InputEventType.vue"

import EventsTypesInterface from '@/interfaces/EventsTypesInterface';

export default defineComponent({
    name: 'EventsTypesRedactor',
    components: {
        EventsTypesTable,
        AddButton,
        DeleteButton,
        InputEventType,
    },
    data() {
        return {
            rowId: -1,
            isInput: false,
            newEventType: '',
        }
    },
    methods: {
        getRowId(rowId: number) {
            console.log(`Deleting row with ID: ${rowId}`);
            this.rowId = rowId;
        },

        setInput(data: boolean) {
            this.isInput = data;
        },

        handleDataFromInputEventType(data: string) {
            this.newEventType = data;
        },

        async deleteEventType(id: number) {
            const res = await EventsTypesInterface.deleteById(id);
            console.log(res)
            this.$router.go(0);
        },

        async addEventType() {
            console.log("Data", this.newEventType);
            const res = await EventsTypesInterface.addEventType(this.newEventType);
            console.log(res)
            this.$router.go(0);
        },
    },
});
</script>
  
  
<style scoped></style>