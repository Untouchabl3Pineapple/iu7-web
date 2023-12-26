<template>
  <div class="black-text" :style="{ fontSize }">
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "TextBlack",
  props: {
    fontSize: String,
  },
});
</script>

<style scoped>
.black-text {
  color: var(--black);
  text-shadow: 0px 0px 0px var(--black);
}
</style>