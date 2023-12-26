<template>
  <!-- <nav>
    <router-link to="/">Home</router-link> |
    <router-link to="/authorization">Auth</router-link>
  </nav> -->

  <notifications position="center" classes="notify" width="100%" />
  <router-view />
</template>

<style>
.notify {
  font-size: 28px;
  background: var(--white) !important;
  margin: 0 5px 5px;
  padding: 10px;
  border-left: 15px solid var(--pink);
  color: var(--black);
  text-shadow: 0px 0px 20px var(--white);
}

body {
  margin: 0;
}

:root {
  --little-text: 20px;
  --middle-text: 25px;
  --large-text: 50px;
  --pink: #731cffea;
  --blue: rgb(32, 48, 79);
  --green: rgb(81, 163, 99);
  --red: rgb(192, 19, 19);
  --white: rgb(255, 255, 255);
  --black: rgb(0, 0, 0);
  --grey: rgba(185, 185, 185, 0.324);

  --selected-event-type: #C4C4C4;

  --green-monitoring-table: #09ae10;
  --yellow-monitoring-table: #f4b207;
  --red-monitoring-table: #f42407;

  --navbar-height: 90px;
  --login-border-width: 200px;
}
</style>