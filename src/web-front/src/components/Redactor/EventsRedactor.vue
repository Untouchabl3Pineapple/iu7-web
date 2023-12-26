<template>
    <div class="event-container">
        <div class="table-container">
            <table class="event-details">
                <thead>
                    <tr>
                        <th>№ Поста</th>
                        <th>Время фиксации</th>
                        <th>Время устранения</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td data-label="№ Поста" v-if="buttonEvent">{{ buttonEvent.number || "-" }}</td>
                        <td data-label="Время фиксации" v-if="event">{{ event.detectingTime || "-" }}</td>
                        <td data-label="Время устранения" v-if="event">{{ event.fixingTime || "-" }}</td>
                    </tr>
                </tbody>

            </table>
        </div>
    </div>


    <div class="description-container">
        <div class="description-label">
            <TextBlack style="font-size: var(--middle-text);">Описание происшествия</TextBlack>
        </div>
        <div class="description-input">
            <InputDescription v-model="description" :font-size="'18px'" :default-text="''" class="full-width" />
        </div>
    </div>


    <TextBlack style="font-size: var(--middle-text);">Тип происшествия</TextBlack>
    <ul class="event-type-list">
        <li v-for="eventType in eventsTypes" :key="eventType.id">
            <EventTypeButton :data="eventType" @button-click="handleButtonClicked" :isActive="isActiveArray[eventType.id]">
                {{ eventType.eventType }}
            </EventTypeButton>
        </li>
    </ul>


    <div class="buttons-container">
        <RedButton @click="redirectToMonitoring()">Отмена</RedButton>
        <div class="button-spacing"></div> <!-- Spacer -->
        <GreenButton @click="sendData(Number(event?.id), currentTypeID, description)">Сохранить</GreenButton>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';

import EventsInterface from "@/interfaces/EventsInterface";
import ButtonsEventsInterface from "@/interfaces/ButtonsEventsInterface";
import EventsTypesInterface from "@/interfaces/EventsTypesInterface";

import { EventType } from '@/interfaces/EventsTypesInterface';
import { Event } from '@/interfaces/EventsInterface';
import { ButtonEvent } from '@/interfaces/ButtonsEventsInterface';

import EventTypeButton from '@/components/Buttons/EventTypeButton.vue';
import GreenButton from '@/components/Buttons/GreenEventsButton.vue';
import RedButton from '@/components/Buttons/RedEventsButton.vue';
import InputDescription from '@/components/Input/InputEventDescription.vue'
import TextBlack from "@/components/Text/TextBlack.vue"

export default defineComponent({
    components: {
        EventTypeButton,
        GreenButton,
        RedButton,
        InputDescription,
        TextBlack,
    },
    data() {
        return {
            event: null as Event | null,
            buttonEvent: null as ButtonEvent | null,
            eventsTypes: [] as EventType[],

            description: '',
            eventType: '',

            isActiveArray: {} as { [key: number]: boolean },
            currentTypeID: 0,
        }
    },
    props: ['id'],
    computed: {
        eventId() {
            return this.$route.params.id;
        }
    },
    mounted() {
        this.getEventByID(Number(this.$route.params.id));
        this.getEventsTypes();
    },
    methods: {
        getEventByID(id: number) {
            EventsInterface.getById(id).then(json => {
                this.event = json.data;
                this.getButtonEventByID(Number(this.event?.buttonEvent_ID));
            });
        },
        getButtonEventByID(id: number) {
            ButtonsEventsInterface.getById(id).then(json => {
                this.buttonEvent = json.data;
            });
        },

        getEventsTypes() {
            EventsTypesInterface.getAll().then(json => {
                this.eventsTypes = json.data;

                this.eventsTypes.forEach(eventType => {
                    this.isActiveArray[eventType.id] = false;
                });

                console.log(this.isActiveArray); // Проверка
            });
        },

        redirectToMonitoring() {
            this.$router.push(`/`);
        },

        async sendData(id: number, typeid: number, descrip: string) {
            console.log(id, typeid, descrip);
            const res = await EventsInterface.put(id, typeid, descrip);
            console.log(res)
            this.$router.push(`/`);
        },

        handleButtonClicked(data: EventType) {
            this.eventsTypes.forEach(eventType => {
                this.isActiveArray[eventType.id] = false;
            });
            this.isActiveArray[data.id] = true;
            this.currentTypeID = data.id

            this.eventType = data.eventType;
            console.log('Значение, переданное при клике на кнопку:', data.eventType);
            // Здесь можно выполнить нужные действия с переданным значением
        },
    }
});
</script>
  
<style scoped>
/* Ваши существующие стили */
/* ... */
.buttons-container {
    display: flex;
    justify-content: flex-end;
    /* Align buttons to the right side */
    margin-top: 60px;
    /* Adjust margin-top as needed */
}

.button-spacing {
    width: 30px;
    /* Adjust the width to set the space between buttons */
}

.description-container {
    display: flex;
    align-items: center;
    margin-bottom: 50px;
    margin-top: 50px;
    /* Регулируйте отступ снизу по вашему усмотрению */
}

.description-label {
    flex: 1;
    min-width: 120px;
    /* Пример: минимальная ширина для текста "Описание происшествия" */
}

.description-input {
    flex: 3;
    /* Пример: Поля ввода занимает большую часть контейнера */
    margin-left: 70px;
    /* flex-shrink: 0; */
    /* Регулируйте отступ от текста "Описание происшествия" до поля ввода */
}

.full-width {
    width: 100%;
    /* Растягиваем поле ввода на всю доступную ширину */
    max-width: 100%;
    /* Добавляем ограничение максимальной ширины поля ввода */
    box-sizing: border-box;
    /* Обеспечиваем правильный расчет ширины с учетом границ и отступов */
}

/* Ваши существующие стили для медиа-запросов */
/* ... */

.event-type-list {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
    /* Отображение по 3 в ряду с автоматической шириной */
    gap: 20px;
    /* Промежуток между элементами */
    list-style: none;
    padding: 0;
}

.event-type-list li {
    display: flex;
    justify-content: center;
    align-items: center;
    border: 1px solid #ffffff;
    border-radius: 5px;
    padding: 10px;
    text-align: center;
}


/* Existing styles for larger screens */

.event-details {
    width: 100%;
    margin-top: 20px;
    border-collapse: collapse;
    border-radius: 3px;
    overflow: hidden;
}

.event-details th,
.event-details td {
    border: 2px solid #000000;
    padding: 12px;
    text-align: center;
}

.event-details th {
    background-color: #3a1cff26;
    font-weight: bold;
}

.event-details tbody tr:nth-child(even) {
    background-color: #f9f9f9;
}

.event-details tbody tr:hover {
    background-color: #f5f5f5;
}

.event-details tbody td {
    font-size: 14px;
}

.event-details tbody td:nth-child(1) {
    font-weight: bold;
}

/* Responsive styles for smaller screens (mobile devices) */
@media screen and (max-width: 768px) {

    .event-details th,
    .event-details td {
        padding: 30px;
        font-size: 12px;
    }

    .event-details th {
        display: none;
    }

    .event-details tbody td {
        display: block;
        text-align: right;
    }

    .event-details tbody td:before {
        content: attr(data-label);
        font-weight: bold;
        float: left;
        text-align: center;
    }

    .event-details tbody td:nth-child(even) {
        background-color: transparent;
    }

    .event-details tbody tr {
        margin-bottom: 10px;
        display: block;
        border: 1px solid #ccc;
        border-radius: 5px;
    }
}
</style>
  