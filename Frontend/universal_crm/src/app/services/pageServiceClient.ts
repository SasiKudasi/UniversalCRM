import { api } from "@/lib/api";

export const pageServiceClient = {
    getPageByPath: async function (path: string) {
        try {
            const response = await api.get("page_by_path?path=" + path);
            
            return response.data;
        } catch (error) {
            console.error("Error fetching page:", error);
            return null;
        }
    }
}