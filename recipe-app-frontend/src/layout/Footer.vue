<script setup lang="ts">
const links = [
  {
    title: 'xd',
    url: 'http://localhost:8080',
  },
];
</script>

<template>
  <v-footer class="bg-grey-lighten-1" style="max-height: 100px">
    <v-row
      justify="center"
      no-gutters
    >
      <v-btn
        v-for="link in links"
        :key="link.url"
        class="mx-2"
        color="white"
        rounded="xl"
        variant="text"
      >
        {{ link.title }}
      </v-btn>
      <v-col
        class="text-center mt-4"
        cols="12"
      >
        {{ new Date().getFullYear() }} — <strong>Vuetify</strong>
      </v-col>
    </v-row>
  </v-footer>
</template>

<style scoped></style>
