<script setup>
import Navbar from '@/components/Navbar.vue'
import { RouterView } from 'vue-router';
</script>

<template>
  <div class="font-handwriting h-screen bg-grape">
    <Navbar />
    <div>
      <RouterView />
    </div>
    
  </div>
</template>