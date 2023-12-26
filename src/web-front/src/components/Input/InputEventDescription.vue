<template>
    <input v-model="inputData" class="input-line" :style="{ fontSize }" :placeholder="defaultText" @input="handleInput">
</template>
  
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    name: "DescriptionInput",
    props: {
        fontSize: {
            type: String,
            default: "16px",
        },
        defaultText: {
            type: String,
            default: "Enter text...",
        },
    },
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
    },
});
</script>
  
<style scoped>
.input-line,
.select-input-line {
    background: var(--grey);
    box-shadow: 0px 0px 20px var(--white);
    border-radius: 20px;
    border: 0.1px solid var(--black);
    padding: 15px 20px;
    color: var(--black);
}

.input-line {
    padding-top: 18px;
    padding-bottom: 18px;
}

.select-input-line {
    border: 1px solid var(--white);
}
</style>
  