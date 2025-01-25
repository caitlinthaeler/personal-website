<script setup>
import Hero from '@/components/Hero.vue'
import HomeCards from '@/components/HomeCards.vue'
import JobListings from '@/components/JobListings.vue'
import ProjectList from '@/components/ProjectList.vue'
</script>

<template>
    <div >
        <Hero />
        <ProjectList :limit="4" :showButton="true"/>
    </div>
</template>