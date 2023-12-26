<template>
    <button :class="{ 'eventtype-button': true, 'active': isActive }" @click="handleButtonClick">
        <TextWhite fontSize="var(--little-text)" fontColor>
            <slot></slot>
        </TextWhite>
    </button>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue'
import TextWhite from '@/components/Text/TextWhite.vue'
import { EventType } from '@/interfaces/EventsTypesInterface';

export default defineComponent({
    name: "EventTypeButton",
    components: {
        TextWhite,
    },
    props: {
        data: {
            type: Object as () => EventType,
            default: () => ({}),
        },
        isActive: {
            type: Boolean,
            default: false,
        },
    },
    methods: {
        handleButtonClick() {
            this.$emit('button-click', this.data);
        }
    }
})
</script>
  
<style>
.eventtype-button {
    background-color: var(--pink);
    border: 2px solid var(--pink);
    box-shadow: 0px 0px 10px var(--gray);
    border-radius: 20px;
    padding: 7px 15px;
    width: auto;
    min-width: 120px;
    transition: box-shadow 0.3s, transform 0.3s;
    cursor: pointer;
}

.eventtype-button:hover {
    box-shadow: 0px 0px 20px var(--pink);
}

.eventtype-button:active {
    box-shadow: inset 0px 0px 10px var(--pink);
    transform: scale(0.95);
}

/* Добавляем стили для активной кнопки */
.active {
    background-color: var(--blue);
    /* Здесь указываете цвет для активной кнопки */
    border-color: var(--blue);
    box-shadow: 0px 0px 10px var(--blue);
    /* Дополнительные стили для активной кнопки */
}

@media screen and (max-width: 768px) {
    .eventtype-button {
        min-width: 100px;
    }
}
</style>
  