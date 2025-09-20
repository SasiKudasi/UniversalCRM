import { Metadata } from "next";
import { pageServiceClient } from "../services/pageServiceClient";
interface Page {
    id: string;
    title: string;
    path: string;
    content: string;
    metaTitle: string;
    metaDescription: string;
    metaKeywords: string | null;
    children: Page[];
}



interface PageProps {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    params: any;
}

export async function generateMetadata({ params }: PageProps): Promise<Metadata> {
    const resolvedParams = await params;
    const path = resolvedParams.slug?.join("/") || "";
    const page = await pageServiceClient.getPageByPath(path);
    if (!page) return { title: "Страница не найдена" };

    return {
        title: page.metaTitle || page.title,
        description: page.metaDescription || "",
        keywords: page.metaKeywords || "",
    };
}

export default async function Page({ params }: PageProps) {
    const resolvedParams = await params;
    const path = resolvedParams.slug?.join("/") || "";
    const page = await pageServiceClient.getPageByPath(path);

    if (!page) return <h1>Страница не найдена</h1>;

    return (
        <div>
            <h1>{page.title}</h1>
            <div dangerouslySetInnerHTML={{ __html: page.content }} />
        </div>
    );
}
