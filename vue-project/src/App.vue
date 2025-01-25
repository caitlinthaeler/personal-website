<script setup>
import Navbar from '@/components/Navbar.vue'
import { RouterView } from 'vue-router';
</script>

<template>
  <div class="font-handwriting text-2xl bg-grape">
    <Navbar />
    <RouterView />
  </div>
</template>