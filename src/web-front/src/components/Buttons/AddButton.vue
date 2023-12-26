<template>
    <button class="full-width add-button">
        <TextWhite fontSize="var(--little-text)" fontColor>
            <slot></slot>
        </TextWhite>
    </button>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue'
import TextWhite from '@/components/Text/TextWhite.vue'

export default defineComponent({
    name: "AddButton",
    components: {
        TextWhite,
    }
})
</script>
  
<style>
.full-width {
    width: 100%;
}

.add-button {
    background-color: var(--green);
    border: 2px solid var(--green);
    box-shadow: 0px 0px 10px var(--gray);
    border-radius: 20px;
    padding: 9.6px 15px;
    cursor: pointer;
    /* margin-top: 30px; */
}

.add-button:hover {
    box-shadow: 0px 0px 20px var(--green);
}

/* Пример медиа-запроса для установки других размеров на различных экранах */
@media screen and (max-width: 768px) {
    .add-button {
        min-width: 100px;
        /* Измените минимальную ширину для более узких экранов */
    }
}
</style>
  