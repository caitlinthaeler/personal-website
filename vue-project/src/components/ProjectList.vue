<script setup>
import { RouterLink } from 'vue-router'
import ProjectListing from '@/components/ProjectListing.vue'
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
    projects: [],
    isLoading: true
});

const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

const getDateOnly = (dateTime) => {
    let [year, month] = dateTime.split("T")[0].split("-").map(Number);
    return `${months[month - 1]} ${year}`;
    
}

const formatDate = (date, placeholder) => {
  return date ? getDateOnly(date) : placeholder;
}

const formatProjectDates = (projects) => {
  return projects.map(project => ({
    ...project,
    formattedStartDate: formatDate(project.startDate, 'N/A'),
    formattedEndDate: formatDate(project.endDate, 'Present'),
  }));
}


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
  <div>
    <section class="bg-grape px-10 py-10">
      <div class="container-xl lg:container m-auto">
        <h2 class="text-3xl font-bold text-plum mb-6 text-center">
          Browse My Projects
        </h2>
        <!-- Show loading spinner while loading is true -->
        <div v-if="state.isLoading" class="text-center text-gray-500 py-6">
          <PulseLoader />
        </div>

        <!-- Show job listing when done loading -->
        <div v-else class="grid grid-cols-1 xs:grid-cols-2 md:grid-cols-3 xl:grid-cols-4 gap-6 auto-rows-auto">
          <ProjectListing class=" w-[200px] h-[100px]" v-for="project in state.projects.slice(0, limit || state.projects.length)" :key="project.id" :project="project" />
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