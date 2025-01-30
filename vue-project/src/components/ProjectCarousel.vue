<script setup>
import { RouterLink } from 'vue-router'
import ProjectListing from '@/components/ProjectListing.vue'
import { reactive, ref, defineProps, onMounted } from 'vue'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import Carousel from 'primevue/carousel';
import axios from 'axios'

defineProps({
    limit: Number,
})

const state = reactive({
    projects: [],
    isLoading: true
});

onMounted(async () => {
    try {
        const response = await axios.get('http://localhost:5283/api/projects-collection/');
        state.projects = formatProjectDates(response.data);

        console.log(state.projects);
    } catch (error){
        console.error('Error fetching projects', error);
    } finally {
        state.isLoading = false;
    }
});
</script>

<template>
     <section class="flex-1 bg-grape-dark py-20">
            <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 flex flex-col items-center">
                Carousel
                <!-- put carousel with mask here -->
                <Carousel :value="state.projects" :numVisible="3" :numScroll="1" circular :autplayInterval="3000"> 
                    
                </Carousel>
            </div>
        </section>
</template>