import { Metadata } from "next";

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

export async function getPageByPath(path: string): Promise<Page | null> {
    const res = await fetch(`http://localhost:5197/api/page_by_path?path=${path}`);
    if (!res.ok) return null;
    return res.json();
}

interface PageProps {
    params: any; // params теперь объект-функция
}

export async function generateMetadata({ params }: PageProps): Promise<Metadata> {
    // нужно await, чтобы получить реальный объект
    const resolvedParams = await params;
    const path = resolvedParams.slug?.join("/") || "";

    const page = await getPageByPath(path);

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
    const page = await getPageByPath(path);

    if (!page) return <h1>Страница не найдена</h1>;

    return (
        <div>
            <h1>{page.title}</h1>
            <div dangerouslySetInnerHTML={{ __html: page.content }} />
        </div>
    );
}
