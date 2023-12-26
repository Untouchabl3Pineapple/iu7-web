<template>
  <button class="green-button">
    <TextWhite fontSize="var(--little-text)" fontColor>
      <slot></slot>
    </TextWhite>
  </button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import TextWhite from '@/components/Text/TextWhite.vue'

export default defineComponent({
  name: "GreenEventsButton",
  components: {
    TextWhite,
  }
})
</script>

<style>
.green-button {
  background-color: var(--green);
  border: 2px solid var(--green);
  box-shadow: 0px 0px 10px var(--gray);
  border-radius: 20px;
  padding: 7px 15px;
  width: auto;
  min-width: 120px;
  cursor: pointer;
  /* Установите минимальную ширину кнопки */
}

.green-button:hover {
  box-shadow: 0px 0px 20px var(--green);
}

/* Пример медиа-запроса для установки других размеров на различных экранах */
@media screen and (max-width: 768px) {
  .green-button {
    min-width: 100px;
    /* Измените минимальную ширину для более узких экранов */
  }
}
</style>
