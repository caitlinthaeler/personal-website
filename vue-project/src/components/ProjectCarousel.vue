<script setup>
import { RouterLink } from 'vue-router'
import ProjectListing from '@/components/ProjectListing.vue'
import { reactive, ref, defineProps, onMounted } from 'vue'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import Carousel from 'primevue/carousel';
import { fetchImage } from '@/utils/fetchImage.js';
import { fetchJson } from '@/utils/fetchJson.js';
import placeholderImage from '@/assets/img/placeholder.png'
import axios from 'axios'

defineProps({
    limit: Number,
})

const state = reactive({
    projects: [],
    isLoading: true
});



const fetchProjects = async () => {
    try {
        const response = await fetchJson('projects.json');
        console.log("projects response", response);
        if (response){
            const projectsData = await Promise.all(Object.keys(response).map(async (key) => {
                const project = response[key];
                
                // Assuming you have a function `fetchThumbnail` that gets the thumbnail for each project
                const thumbnail = await fetchImage(project.thumbnail.filePath);  // Example URL for the thumbnail
                return {
                    ...project,
                    imageUrl: thumbnail
                };
            }));
            state.projects = projectsData;
            console.log(state.imageUrls);
        } else {
          console.error('Couldn\'t find that file');
        }
        
    } catch (error) {
        console.error('Error loading JSON files:', error);
    } finally{
      state.isLoading = false;
    }
};

onMounted(fetchProjects);
</script>

<template>
    <section class="flex flex-col">
        <div class="relative flex justify-center w-full h-[300px] overflow-hidden">
            <div class="mask-image absolute flex justify-center w-[1200px] h-[300px] aspect-ratio max-w-5xl mx-auto">
                 <!-- <div class="absolute inset-0 pointer-events-none z-10"
                style="
                    -webkit-mask-image: linear-gradient(circle, rgba(0, 0, 0, 1) 20%, rgba(0, 0, 0, 1) 40%, rgba(0, 0, 0, 1) 80%, rgba(0, 0, 0, 0) 100%);
                    mask-image: linear-gradient(circle, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 1) 20%, rgba(0, 0, 0, 1) 80%, rgba(0, 0, 0, 0) 100%);
                ">
            </div> -->

                <Carousel 
                    :value="state.projects.concat(state.projects).concat(state.projects)" 
                    :numVisible="3" 
                    :numScroll="1"
                    :autoplay="true"
                    :circular="false"
                    :autoplayInterval="3000"
                    class="relative z-10 w-full h-full"
                    >
                    <template #item="slotProps">
                        <div class="p-4">
                            <img :src="slotProps.data.imageUrl" alt="Project Thumbnail" class="border-4 border-plum rounded-lg shadow-md h-[200px]">
                            <h3 class="text-lg font-semibold mt-2 text-center text-lemon">
                                {{ slotProps.data.title }}
                            </h3>
                        </div>
                    </template>
                    
                </Carousel>
            </div>
           
        </div>
        <div class="flex flex-col">
            <div class="gap-5 justify-center text-center text-sm my-10">
                    <RouterLink to="/projects" class="text-grape bg-lemon hover:bg-plum rounded-md px-3 py-2 mt-4"
                    >View AllProjects</RouterLink>
            </div>
        </div>
        <div>
            <p class="text-center text-plum">
                *website looks different with dark mode and extensions like dark reader. I will work on this eventually
            </p>
        </div>
        
    </section>
</template>

<style scoped>
.mask-image {
    mask-image: linear-gradient(to right, rgba(0, 0, 0, 0) 25%, rgba(0, 0, 0, 1) 35%, rgba(0, 0, 0, 1) 65%, rgba(0, 0, 0, 0) 75%);
}
::v-deep(.p-carousel-prev),
::v-deep(.p-carousel-next) {
    position: absolute;
    top: 50%;
    transform: translateY(-50%);
    z-index: 20; /* Ensure it's above the mask */
}

::v-deep(.p-carousel-prev) {
    left: -50px; /* Move left arrow outside the mask */
}

::v-deep(.p-carousel-next) {
    right: -50px; /* Move right arrow outside the mask */
}

</style>
  
  