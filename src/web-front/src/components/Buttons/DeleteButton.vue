<template>
    <button class="full-width delete-button">
        <TextWhite fontSize="var(--little-text)" fontColor>
            <slot></slot>
        </TextWhite>
    </button>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue'
import TextWhite from '@/components/Text/TextWhite.vue'

export default defineComponent({
    name: "DeleteButton",
    components: {
        TextWhite,
    }
})
</script>
  
<style>
.full-width {
    width: 100%;
}

.delete-button {
    background-color: var(--red);
    border: 2px solid var(--red);
    box-shadow: 0px 0px 10px var(--gray);
    border-radius: 20px;
    padding: 9.6px 15px;
    cursor: pointer;
    /* margin-top: 30px; */
}

.delete-button:hover {
    box-shadow: 0px 0px 20px var(--red);
}

/* Пример медиа-запроса для установки других размеров на различных экранах */
@media screen and (max-width: 768px) {
    .delete-button {
        min-width: 100px;
        /* Измените минимальную ширину для более узких экранов */
    }
}
</style>
  