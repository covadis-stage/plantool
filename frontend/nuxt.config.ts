import CustomTheme from "./primevue-theme";
import primevueConfig from "./primevue-theme";

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  nitro: {
    preset: "azure",
  },
  modules: ['@primevue/nuxt-module', '@pinia/nuxt'],
  primevue: {
    options: {
      locale: {
        dateFormat: 'dd-mm-yy',
        // Default values https://primevue.org/configuration/#localeapi
        fileSizeTypes: ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
        dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
        dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
        dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
        monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
        monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
      },
      theme: {
        preset: CustomTheme,
        options: {
          darkModeSelector: false || 'none',
        }
      },
    },
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
  ],
  components: [
    {
      path: "~/components",
      pathPrefix: false,
    },
  ],
})