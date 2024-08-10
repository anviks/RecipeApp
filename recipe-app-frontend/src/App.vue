<script setup lang="ts">

import Header from '@/components/Header.vue';
import Footer from '@/components/Footer.vue';

</script>

<template>
    <Header />
    <div class="container">
        <main role="main" class="pb-3">
            <router-view></router-view>
        </main>
    </div>
    <Footer />
</template>

<style scoped></style>
    