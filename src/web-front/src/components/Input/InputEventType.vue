<template>
    <div class="input-container">
        <img src="@/assets/AddEventType.svg" class="input-image add-icon" alt="AddEventType" @click="emitAddEvent">
        <img src="@/assets/DeleteEventType.svg" class="input-image delete-icon" alt="DeleteEventType"
            @click="emitDeleteEvent">
        <input v-model="inputData" class="input-line" :style="{ fontSize }" :placeholder="defaultText" @input="handleInput">
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "InputEventType",
    props: {
        fontSize: {
            type: String,
            default: "16px",
        },
        defaultText: {
            type: String,
            default: "",
        },
    },
    emits: ['input', 'add-event-clicked', 'delete-event-clicked'], // Добавленная опция emits
    data() {
        return {
            inputData: "",
        };
    },
    computed: {
    },
    methods: {
        handleInput() {
            this.$emit("input", this.inputData);
        },
        emitAddEvent() {
            this.$emit("add-event-clicked");
        },
        emitDeleteEvent() {
            this.$emit("delete-event-clicked");
        },
    },
});
</script>
  
<style scoped>
img {
    cursor: pointer;
}

/* Общий контейнер для изображений и поля ввода */
.input-container {
    position: relative;
}

/* Стили для изображений */
.input-image {
    position: absolute;
    top: 50%;
    transform: translateY(-50%);
    width: 30px;
}

.add-icon {
    left: 10px;
    /* Расположение первой иконки */
}

.delete-icon {
    left: 45px;
    /* Расположение второй иконки */
}

.input-line {
    /* Ваши стили для поля ввода остаются без изменений */
    /* margin-top: 30px; */
    width: 100%;
    background: var(--white);
    border-radius: 20px;
    border: 2px solid var(--black);
    padding: 15px 20px;
    color: var(--black);
    box-sizing: border-box;
    padding-left: 100px;
    padding-top: 12px;
    padding-bottom: 12px;
}
</style>
  
  