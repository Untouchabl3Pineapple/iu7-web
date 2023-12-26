<template>
    <div class="tableWrapper">
        <div class="monitoringDiv">
            <table class="monitoring-table">
                <tbody class="monitoringTBody">
                    <!-- Top row -->
                    <tr class="monitoringTr">
                        <td v-for="bpost in buttonsPosts" :key="`${bpost.leftSide}_${bpost.leftColor}_${bpost.post}t`"
                            :id="`${bpost.leftSide}_${bpost.leftColor}_${bpost.post}t`" class="accdient-registration-top"
                            :style="getTopColor(bpost)">
                        </td>
                    </tr>

                    <!-- Middle row with post numbers -->
                    <tr class="monitoringTr">
                        <td v-for="bpost in buttonsPosts" :key="bpost.post" :id="bpost.post" class="post-nmb">
                            {{ bpost.post }}
                        </td>
                    </tr>

                    <!-- Bottom row -->
                    <tr class="monitoringTr">
                        <td v-for="bpost in buttonsPosts" :key="`${bpost.rightSide}_${bpost.rightColor}_${bpost.post}b`"
                            :id="`${bpost.rightSide}_${bpost.rightColor}_${bpost.post}b`"
                            class="accdient-registration-bottom" :style="getBottomColor(bpost)">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
    
<script lang="ts">
import { defineComponent } from 'vue';
import ButtonsPostsInterface from "@/interfaces/ButtonsPostsInterface";
import { ButtonPost } from "@/interfaces/ButtonsPostsInterface"

export default defineComponent({
    name: 'MonitoringTable',
    data() {
        return {
            buttonsPosts: [] as ButtonPost[],
        }
    },
    mounted() {
        this.getButtonsPosts();
    },
    methods: {
        getButtonsPosts() {
            ButtonsPostsInterface.getAll().then(json => {
                this.buttonsPosts = json.data;
                this.sortedPosts();
            });
        },
        sortedPosts(): ButtonPost[] {
            return this.buttonsPosts.sort((a, b) => a.post - b.post);
        },
        getTopColor(bpost: ButtonPost): string {
            if (bpost.leftColor === 'RED') {
                return 'background-color: #f42407';
            } else if (bpost.leftColor === 'YELLOW') {
                return 'background-color: #f4b207';
            } else {
                return 'background-color: #09ae10';
            }
        },
        getBottomColor(bpost: ButtonPost): string {
            if (bpost.rightColor === 'RED') {
                return 'background-color: #f42407';
            } else if (bpost.rightColor === 'YELLOW') {
                return 'background-color: #f4b207';
            } else {
                return 'background-color: #09ae10';
            }
        },
    },
});
</script>
    
<style scoped>
/* Table styles */
.tableWrapper {
    width: 100%;
    overflow-x: auto;
    /* margin-top: 20px; */
    padding: 0;
    display: flex;
    justify-content: center;
}

.monitoringDiv {
    padding: 0 10px;
    width: 100%;
    margin: 0;
}

.monitoring-table {
    width: 100%;
    /* margin-top: 30px; */
    margin-left: 0;
    margin-right: 0;
    border-spacing: 0;
    table-layout: fixed;
    /* Фиксированная ширина столбцов */
}

/* Стили для ячеек с номерами постов */
.post-nmb {
    border-collapse: collapse;
    border: solid;
    width: 8%;
    /* Пример использования относительной ширины */
    border-color: black;
    border-width: 2px;
    font-family: Verdana, Tahoma, sans-serif;
    font-size: larger;
    text-align: center;
    color: black;
}

.accdient-registration-top {
    border-collapse: separate;
    height: 79px;
    border-radius: 20px 20px 0px 0px;
    border: 2px solid rgb(0, 0, 0);
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}

.accdient-registration-bottom {
    border-collapse: collapse;
    height: 79px;
    border-radius: 0px 0px 20px 20px;
    border: 2px solid rgb(0, 0, 0);
    margin-left: 0%;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}

/* Media query for smaller screens */
</style>
  