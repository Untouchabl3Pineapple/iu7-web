<template>
    <div class="table-container" v-if="isInRole !== 'guest'">
        <table class="eventsTypes-table" v-if="displayedEventsTypes.length > 0">
            <thead>
                <tr>
                    <th>Номер</th>
                    <th>Название</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(eventType, index) in displayedEventsTypes" :key="eventType.id"
                    :class="{ 'selected-row': selectedRowId === eventType.id }" @click="selectRow(eventType.id)">
                    <td data-label="Номер">{{ index + 1 }}</td>
                    <td data-label="Название">{{ eventType.eventType }}</td>
                </tr>
            </tbody>
        </table>
        <div class="pagination">
            <button v-for="pageNumber in totalPages" :key="pageNumber" @click="changePage(pageNumber)">
                {{ pageNumber }}
            </button>
        </div>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';
import EventsTypesInterface from "@/interfaces/EventsTypesInterface";
import { EventType } from '@/interfaces/EventsTypesInterface';
import auth from '@/authentificationService';

export default defineComponent({
    data() {
        return {
            eventsTypes: [] as EventType[],
            currentPage: 1,
            itemsPerPage: 10,
            counter: 0,
            selectedRowId: -1,
        };
    },
    computed: {
        displayedEventsTypes(): EventType[] {
            const startIndex = (this.currentPage - 1) * this.itemsPerPage;
            const endIndex = startIndex + this.itemsPerPage;

            const sortedEventsTypes = this.eventsTypes.slice().sort((a, b) => {
                return a.id - b.id;
            });

            return sortedEventsTypes.slice(startIndex, endIndex);
        },
        totalPages(): number {
            return Math.ceil(this.eventsTypes.length / this.itemsPerPage);
        },
        isInRole() {
            if (auth.getCurrentUser()) {
                console.log(auth.getCurrentUser().permission);
                return auth.getCurrentUser().permission;
            } else {
                auth.logout();
                return "guest";
            }
        },
    },
    methods: {
        getEventsTypes() {
            EventsTypesInterface.getAll().then(json => {
                this.eventsTypes = json.data;
            });
        },
        changePage(pageNumber: number) {
            this.currentPage = pageNumber;
        },
        selectRow(rowId: number) {
            if (this.selectedRowId == rowId) { this.selectedRowId = -1; } else { this.selectedRowId = rowId; }
            this.$emit('row-selected', this.selectedRowId);
        },
    },
    mounted() {
        this.getEventsTypes();
    },
});
</script>
  
<style scoped>
.selected-row {
    background-color: var(--selected-event-type);
    /* Выбранный цвет фона строки */
    /* Цвет текста для лучшей видимости */
}

/* Existing styles for larger screens */
.eventsTypes-table {
    width: 100%;
    margin-top: 10px;
    border-collapse: collapse;
    border-radius: 3px;
    overflow: hidden;
}

.eventsTypes-table th,
.eventsTypes-table td {
    border: 2px solid #000000;
    padding: 12px;
    text-align: center;
}

.eventsTypes-table th {
    background-color: #f2f2f2;
    font-weight: bold;
}

.eventsTypes-table tbody tr:nth-child(even) {
    /* background-color: #f9f9f9; */
}

.eventsTypes-table tbody tr:hover {
    /* background-color: #f5f5f5; */
}

.eventsTypes-table tbody td {
    font-size: 14px;
}

.eventsTypes-table tbody td:nth-child(1) {
    font-weight: bold;
}

.pagination {
    margin-top: 20px;
    display: flex;
    justify-content: center;
}

.pagination button {
    margin: 0 5px;
    padding: 5px 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #f0f0f0;
    cursor: pointer;
}

.pagination button:hover {
    background-color: #e0e0e0;
}

/* Responsive styles for smaller screens (mobile devices) */
@media screen and (max-width: 768px) {

    .eventsTypes-table th,
    .eventsTypes-table td {
        padding: 30px;
        font-size: 12px;
    }

    .eventsTypes-table th {
        display: none;
    }

    .eventsTypes-table tbody td {
        display: block;
        text-align: right;
    }

    .eventsTypes-table tbody td:before {
        content: attr(data-label);
        font-weight: bold;
        float: left;
        text-align: center;
    }

    .eventsTypes-table tbody td:nth-child(even) {
        background-color: transparent;
    }

    .eventsTypes-table tbody tr {
        margin-bottom: 10px;
        display: block;
        border: 1px solid #ccc;
        border-radius: 5px;
    }

    .pagination button {
        font-size: 12px;
        padding: 3px 8px;
        margin: 0 3px;
    }
}
</style>


<!-- Adjusted table structure for mobile -->
