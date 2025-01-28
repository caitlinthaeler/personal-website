<script setup>
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import BackButton from '@/components/BackButton.vue'
import UnderConstruction from '@/components/UnderConstruction.vue'
import { reactive, onMounted } from 'vue'
import { useRoute, RouterLink, useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import axios from 'axios'

const route = useRoute();
const router = useRouter();
const toast = useToast();

const projectId = route.params.id;

const state = reactive({
    project: {},
    isLoading: true,
    isReady: false
});

// onMounted(async () => {
//     try {
//         const response = await axios.get(`/api/projects/${jobId}`);
//         state.project = response.data;
//     } catch (error) {
//         console.error('Error fetching project', error);
//     } finally {
//         state.isLoading = false;
//     }
// });
</script>

<template>
    <div>
        <!-- show page according to id -->
        <div>
            <div v-if="!isReady">
                <UnderConstruction />
            </div>
            <div v-else>
            
            <!-- show unique page component -->
            </div>
        </div>
      
    </div>
</template>