<template>
  <button class="red-button">
    <TextWhite fontSize="var(--little-text)" fontColor>
      <slot></slot>
    </TextWhite>
  </button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import TextWhite from '@/components/Text/TextWhite.vue'

export default defineComponent({
  name: "RedEventsButton",
  components: {
    TextWhite,
  }
})
</script>

<style>
.red-button {
  background-color: var(--red);
  border: 2px solid var(--red);
  box-shadow: 0px 0px 10px var(--gray);
  border-radius: 20px;
  padding: 7px 15px;
  width: 10%;
  min-width: 120px;
  cursor: pointer;
  /* Установите минимальную ширину кнопки */
}

.red-button:hover {
  box-shadow: 0px 0px 20px var(--red);
}

/* Пример медиа-запроса для установки других размеров на различных экранах */
@media screen and (max-width: 768px) {
  .red-button {
    width: 10%;
    /* Измените ширину для более узких экранов */
  }
}
</style>
