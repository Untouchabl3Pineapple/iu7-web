<template>
  <button class="navbar-button">
    <TextBlack fontSize="var(--little-text)" fontColor>
      <slot></slot>
    </TextBlack>
  </button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import TextBlack from '@/components/Text/TextBlack.vue'

export default defineComponent({
  name: "NavbarButton",
  components: {
    TextBlack
  }
})
</script>

<style>
.navbar-button {
  background-color: var(--white);
  border: 2px solid var(--white);
  /* box-shadow: 0px 0px 10px var(--white); */
  border-radius: 50px;
  /* Make the border-radius a larger value for an oval shape */
  padding-left: 15px;
  padding-right: 15px;
  padding-top: 7px;
  padding-bottom: 7px;
  min-width: 117px;
  display: inline-block;
  cursor: pointer;
  /* Ensures the button takes the shape of the content */
}

.navbar-button:hover {
  box-shadow: 0px 0px 20px var(--white);
}
</style>
