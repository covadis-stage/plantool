import Lara from "@primeuix/themes/lara";

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  nitro: {
    preset: "azure",
  },
  modules: [
    '@primevue/nuxt-module',
  ],
  primevue: {
    options: {
      theme: {
        preset: Lara,
        options: {
          darkModeSelector: false || 'none',
        }
      }
    }
  },
  app: {
    head: {
      link: [
        { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },
      ],
    }
  },
  runtimeConfig: {
    public: {
      apiUrl: process.env.NUXT_PUBLIC_API_URL ?? ""
    }
  },
  css: [
    '@/assets/style/imports.scss',
  ]
})
