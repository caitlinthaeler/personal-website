<script setup>
import { RouterLink } from 'vue-router'
import { ref, computed } from 'vue'

const props = defineProps({
    frameWidth: {
        type: Number,
        default: 1024
    },
    frameHeight: {
        type: Number,
        default: 576
    },
    scale: {
        type: Number,
        default: 1
    },
    resumeUrl: {
        type: String,
        default: '/files/caitlin-thaeler-cv-2025-5.pdf'
    }
});

const resume = computed(() => {
  // Handle both local and external URLs
  if (props.resumeUrl.startsWith('@/')) {
    // Convert alias to relative path
    const path = props.resumeUrl.replace('@/', '../');
    return new URL(path, import.meta.url).href;
  } else {
    return props.resumeUrl;
  }
});

function downloadResume() {
  const link = document.createElement('a');
  link.href = resume.value;
  link.download = 'caitlin-thaeler-cv.pdf'; // or any custom name
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
}
</script>

<template>
    <div class="bg-grape h-full">
        <div class="p-10">
            <h1 class="text-grape-light text-7xl">
                Resume
            </h1>
            
            <div class="flex flex-row justify-between pb-2">
                <p class="mt-10 text-grape-light text-md">
                Updated in May 2025
                </p>
                <button @click="downloadResume" class="text-grape bg-lemon hover:bg-plum rounded-md px-3 py-2 mt-4">
                    Download Resume
                </button>
            </div>
            <div class="bg-lemon p-5">
                <div v-if="resume">
                    <iframe :src="resume" width="100%" height="700px"></iframe>
                </div>
                <p v-else class="p-5 text-center">
                    Nothing here yet
                </p>
            </div>
        </div>
       
    </div>
</template>