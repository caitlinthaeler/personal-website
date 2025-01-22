<script setup>
import { RouterLink } from 'vue-router'
import JobListing from '@/components/JobListing.vue'
import { reactive, ref, defineProps, onMounted } from 'vue'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import axios from 'axios'

defineProps({
    limit: Number,
    showButton: {
        type: Boolean,
        default: false
    }
})

const state = reactive({
    jobs: [],
    isLoading: true
});


onMounted(async () => {
    try {
        const response = await axios.get('/api/projects');
        state.projects = response.data;
    } catch (error){
        console.error('Error fetching projects', error);
    } finally {
        state.isLoading = false;
    }
});
</script>

<template>
  <div>
    <section class="bg-blue-50 px-4 py-10">
      <div class="container-xl lg:container m-auto">
        <h2 class="text-3xl font-bold text-green-500 mb-6 text-center">
          Browse Jobs
        </h2>
        <!-- Show loading spinner while loading is true -->
        <div v-if="state.isLoading" class="text-center text-gray-500 py-6">
          <PulseLoader />
        </div>

        <!-- Show job listing when done loading -->
        <div v-else class="grid grid-cols-1 md:grid-cols-3 gap-6">
          <JobListing v-for="project in state.projects.slice(0, limit || state.projects.length)" :key="project.id" :project="project" />
        </div>
      </div>
    </section>

    <section v-if="showButton" class="m-auto max-w-lg my-10 px-6">
      <RouterLink to="/projects" class="block bg-black text-white text-center py-4 px-6 rounded-xl hover:bg-gray-700">
        View All Projects
      </RouterLink>
    </section>
  </div>
</template>