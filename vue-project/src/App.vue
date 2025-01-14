<script setup>
import Navbar from '@/components/Navbar.vue'
import Hero from '@/components/Hero.vue'
import HomeCards from '@/components/HomeCards.vue'
import JobListings from '@/components/JobListings.vue'
</script>

<template>
  <div class="font-handwriting text-2xl">
    <Navbar />
    <Hero/>
    <HomeCards />
    <JobListings :limit="3" :showButton="true"/>
  </div>
</template>