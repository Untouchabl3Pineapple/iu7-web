<template>
    <div class="table-container" v-if="isInRole != 'guest'">
        <table class="event-table" v-if="displayedEvents.length > 0">
            <thead>
                <tr>
                    <th>№ Поста</th>
                    <th>Тип происшествия</th>
                    <th>Описание</th>
                    <th>Время фиксации</th>
                    <th>Время устранения</th>
                    <th>Редактор</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="event in displayedEvents" :key="event.id">
                    <td data-label="№ Поста         ">{{ getButtonEvent(event.buttonEvent_ID) }}</td>
                    <td data-label="Тип происшествия">{{ getEventType(event.eventType_ID) || '-' }}</td>
                    <td data-label="Описание        ">{{ event.eventDescription || '-' }}</td>
                    <td data-label="Время фиксации  ">{{ event.detectingTime }}</td>
                    <td data-label="Время устранения">{{ event.fixingTime || '-' }}</td>
                    <td data-label="Редактор        "><img src="@/assets/redactor.svg" alt="redactor"
                            @click="redirectToEvent(event.id)" /></td>
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
import EventsInterface from "@/interfaces/EventsInterface";
import EventsTypesInterface from "@/interfaces/EventsTypesInterface";
import ButtonsEventsInterface from "@/interfaces/ButtonsEventsInterface";

import { Event } from '@/interfaces/EventsInterface';
import { EventType } from '@/interfaces/EventsTypesInterface';
import { ButtonEvent } from '@/interfaces/ButtonsEventsInterface';

import auth from '@/authentificationService'

export default defineComponent({
    data() {
        return {
            events: [] as Event[],
            eventsTypes: [] as EventType[],
            buttonsEvents: [] as ButtonEvent[],
            currentPage: 1,
            itemsPerPage: 10,
        }
    },
    computed: {
        displayedEvents(): Event[] {
            const startIndex = (this.currentPage - 1) * this.itemsPerPage;
            const endIndex = startIndex + this.itemsPerPage;
            return this.events.slice(startIndex, endIndex);
        },
        totalPages(): number {
            return Math.ceil(this.events.length / this.itemsPerPage);
        },

        isInRole() {
            if (auth.getCurrentUser()) {
                console.log(auth.getCurrentUser().permission);
                return auth.getCurrentUser().permission;
            }
            else {
                auth.logout()
                return "guest"
            }
        },

        // isMobile() {
        //     return window.innerWidth <= 768;
        // }
    },
    methods: {
        getEvents() {
            EventsInterface.getAll().then(json => {
                this.events = json.data;
                this.sortingEvents()
            });
        },
        sortingEvents() {
            this.events.sort((a, b) => {
                const dateA = new Date(a.detectingTime).getTime();
                const dateB = new Date(b.detectingTime).getTime();
                return dateB - dateA; // Сортировка по убыванию detectingTime
            });
        },

        getEventsTypes() {
            EventsTypesInterface.getAll().then(json => {
                this.eventsTypes = json.data;
            });
        },
        getEventType(id: number): string | undefined {
            return this.eventsTypes.find(event => event.id === id)?.eventType;
        },
        getButtonsEvents() {
            ButtonsEventsInterface.getAll().then(json => {
                this.buttonsEvents = json.data;
            });
        },
        getButtonEvent(id: number): number | undefined {
            return this.buttonsEvents.find(event => event.id === id)?.number;
        },
        changePage(pageNumber: number) {
            this.currentPage = pageNumber;
        },

        redirectToEvent(eventId: number) {
            this.$router.push(`/events/${eventId}`);
        },
    },
    mounted() {
        this.getEvents();
        this.getEventsTypes();
        this.getButtonsEvents();
    },
});
</script>
  
<style scoped>
/* Existing styles for larger screens */
img {
    cursor: pointer;
}

.event-table {
    width: 100%;
    margin-top: 40px;
    border-collapse: collapse;
    border-radius: 3px;
    overflow: hidden;
}

.event-table th,
.event-table td {
    border: 2px solid #000000;
    padding: 12px;
    text-align: center;
}

.event-table th {
    background-color: #f2f2f2;
    font-weight: bold;
}

.event-table tbody tr:nth-child(even) {
    /* background-color: #f9f9f9; */
}

.event-table tbody tr:hover {
    background-color: #f5f5f5;
}

.event-table tbody td {
    font-size: 14px;
}

.event-table tbody td:nth-child(1) {
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

    .event-table th,
    .event-table td {
        padding: 30px;
        font-size: 12px;
    }

    .event-table th {
        display: none;
    }

    .event-table tbody td {
        display: block;
        text-align: right;
    }

    .event-table tbody td:before {
        content: attr(data-label);
        font-weight: bold;
        float: left;
        text-align: center;
    }

    .event-table tbody td:nth-child(even) {
        background-color: transparent;
    }

    .event-table tbody tr {
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
