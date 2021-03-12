using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models.ApiModels
{
    public class RedditResponse
    {

            public string kind { get; set; }
            public Data data { get; set; }
        

        public class Data
        {
            public string after { get; set; }
            public int dist { get; set; }
            public Facets facets { get; set; }
            public string modhash { get; set; }
            public Child[] children { get; set; }
            public object before { get; set; }
        }

        public class Facets
        {
        }

        public class Child
        {
            public string kind { get; set; }
            public Data1 data { get; set; }
        }

        public class Data1
        {
            public object approved_at_utc { get; set; }
            public string subreddit { get; set; }
            public string selftext { get; set; }
            public string author_fullname { get; set; }
            public bool saved { get; set; }
            public object mod_reason_title { get; set; }
            public int gilded { get; set; }
            public bool clicked { get; set; }
            public string title { get; set; }
            public Link_Flair_Richtext[] link_flair_richtext { get; set; }
            public string subreddit_name_prefixed { get; set; }
            public bool hidden { get; set; }
            public int pwls { get; set; }
            public string link_flair_css_class { get; set; }
            public int downs { get; set; }
            public int? thumbnail_height { get; set; }
            public string top_awarded_type { get; set; }
            public bool hide_score { get; set; }
            public string name { get; set; }
            public bool quarantine { get; set; }
            public string link_flair_text_color { get; set; }
            public float upvote_ratio { get; set; }
            public string author_flair_background_color { get; set; }
            public string subreddit_type { get; set; }
            public int ups { get; set; }
            public int total_awards_received { get; set; }
            public Media_Embed media_embed { get; set; }
            public int? thumbnail_width { get; set; }
            public string author_flair_template_id { get; set; }
            public bool is_original_content { get; set; }
            public object[] user_reports { get; set; }
            public Secure_Media secure_media { get; set; }
            public bool is_reddit_media_domain { get; set; }
            public bool is_meta { get; set; }
            public object category { get; set; }
            public Secure_Media_Embed secure_media_embed { get; set; }
            public string link_flair_text { get; set; }
            public bool can_mod_post { get; set; }
            public int score { get; set; }
            public object approved_by { get; set; }
            public bool author_premium { get; set; }
            public string thumbnail { get; set; }
            public object edited { get; set; }
            public string author_flair_css_class { get; set; }
            public Author_Flair_Richtext[] author_flair_richtext { get; set; }
            public Gildings gildings { get; set; }
            public string post_hint { get; set; }
            public object content_categories { get; set; }
            public bool is_self { get; set; }
            public object mod_note { get; set; }
            //public int created { get; set; }
            public string link_flair_type { get; set; }
            public int wls { get; set; }
            public object removed_by_category { get; set; }
            public object banned_by { get; set; }
            public string author_flair_type { get; set; }
            public string domain { get; set; }
            public bool allow_live_comments { get; set; }
            public string selftext_html { get; set; }
            public object likes { get; set; }
            public string suggested_sort { get; set; }
            public object banned_at_utc { get; set; }
            public string url_overridden_by_dest { get; set; }
            public object view_count { get; set; }
            public bool archived { get; set; }
            public bool no_follow { get; set; }
            public bool is_crosspostable { get; set; }
            public bool pinned { get; set; }
            public bool over_18 { get; set; }
            public Preview preview { get; set; }
            public All_Awardings[] all_awardings { get; set; }
            public object[] awarders { get; set; }
            public bool media_only { get; set; }
            public string link_flair_template_id { get; set; }
            public bool can_gild { get; set; }
            public bool spoiler { get; set; }
            public bool locked { get; set; }
            public string author_flair_text { get; set; }
            public string[] treatment_tags { get; set; }
            public bool visited { get; set; }
            public object removed_by { get; set; }
            public object num_reports { get; set; }
            public object distinguished { get; set; }
            public string subreddit_id { get; set; }
            public object mod_reason_by { get; set; }
            public object removal_reason { get; set; }
            public string link_flair_background_color { get; set; }
            public string id { get; set; }
            public bool is_robot_indexable { get; set; }
            public object report_reasons { get; set; }
            public string author { get; set; }
            public string discussion_type { get; set; }
            public int num_comments { get; set; }
            public bool send_replies { get; set; }
            public string whitelist_status { get; set; }
            public bool contest_mode { get; set; }
            public object[] mod_reports { get; set; }
            public bool author_patreon_flair { get; set; }
            public string author_flair_text_color { get; set; }
            public string permalink { get; set; }
            public string parent_whitelist_status { get; set; }
            public bool stickied { get; set; }
            public string url { get; set; }
            public int subreddit_subscribers { get; set; }
            public int num_crossposts { get; set; }
            public Media media { get; set; }
            public bool is_video { get; set; }
        }

        public class Media_Embed
        {
        }

        public class Secure_Media
        {
            public Reddit_Video reddit_video { get; set; }
        }

        public class Reddit_Video
        {
            public int bitrate_kbps { get; set; }
            public string fallback_url { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public string scrubber_media_url { get; set; }
            public string dash_url { get; set; }
            public int duration { get; set; }
            public string hls_url { get; set; }
            public bool is_gif { get; set; }
            public string transcoding_status { get; set; }
        }

        public class Secure_Media_Embed
        {
        }

        public class Gildings
        {
            public int gid_1 { get; set; }
            public int gid_2 { get; set; }
            public int gid_3 { get; set; }
        }

        public class Preview
        {
            public Image[] images { get; set; }
            public bool enabled { get; set; }
        }

        public class Image
        {
            public Source source { get; set; }
            public Resolution2[] resolutions { get; set; }
            public Variants variants { get; set; }
            public string id { get; set; }
        }

        public class Source
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Variants
        {
            public Gif gif { get; set; }
            public Mp4 mp4 { get; set; }
        }

        public class Gif
        {
            public Source1 source { get; set; }
            public Resolution[] resolutions { get; set; }
        }

        public class Source1
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resolution
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Mp4
        {
            public Source2 source { get; set; }
            public Resolution1[] resolutions { get; set; }
        }

        public class Source2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resolution1
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resolution2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Media
        {
            public Reddit_Video1 reddit_video { get; set; }
        }

        public class Reddit_Video1
        {
            public int bitrate_kbps { get; set; }
            public string fallback_url { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public string scrubber_media_url { get; set; }
            public string dash_url { get; set; }
            public int duration { get; set; }
            public string hls_url { get; set; }
            public bool is_gif { get; set; }
            public string transcoding_status { get; set; }
        }

        public class Link_Flair_Richtext
        {
            public string e { get; set; }
            public string t { get; set; }
        }

        public class Author_Flair_Richtext
        {
            public string e { get; set; }
            public string t { get; set; }
            public string a { get; set; }
            public string u { get; set; }
        }

        public class All_Awardings
        {
            public int? giver_coin_reward { get; set; }
            public object subreddit_id { get; set; }
            public bool is_new { get; set; }
            public int days_of_drip_extension { get; set; }
            public int coin_price { get; set; }
            public string id { get; set; }
            public int? penny_donate { get; set; }
            public string award_sub_type { get; set; }
            public int coin_reward { get; set; }
            public string icon_url { get; set; }
            public int days_of_premium { get; set; }
            public Tiers_By_Required_Awardings tiers_by_required_awardings { get; set; }
            public Resized_Icons7[] resized_icons { get; set; }
            public int icon_width { get; set; }
            public int static_icon_width { get; set; }
            public int? start_date { get; set; }
            public bool is_enabled { get; set; }
            public int? awardings_required_to_grant_benefits { get; set; }
            public string description { get; set; }
            public object end_date { get; set; }
            public int subreddit_coin_reward { get; set; }
            public int count { get; set; }
            public int static_icon_height { get; set; }
            public string name { get; set; }
            public Resized_Static_Icons7[] resized_static_icons { get; set; }
            public string icon_format { get; set; }
            public int icon_height { get; set; }
            public int? penny_price { get; set; }
            public string award_type { get; set; }
            public string static_icon_url { get; set; }
        }

        public class Tiers_By_Required_Awardings
        {
            public _0 _0 { get; set; }
            public _3 _3 { get; set; }
            public _6 _6 { get; set; }
            public _9 _9 { get; set; }
            public _5 _5 { get; set; }
            public _10 _10 { get; set; }
            public _25 _25 { get; set; }
        }

        public class _0
        {
            public Resized_Icons[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon static_icon { get; set; }
            public Resized_Static_Icons[] resized_static_icons { get; set; }
            public Icon icon { get; set; }
        }

        public class Static_Icon
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class _3
        {
            public Resized_Icons1[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon1 static_icon { get; set; }
            public Resized_Static_Icons1[] resized_static_icons { get; set; }
            public Icon1 icon { get; set; }
        }

        public class Static_Icon1
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon1
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons1
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons1
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class _6
        {
            public Resized_Icons2[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon2 static_icon { get; set; }
            public Resized_Static_Icons2[] resized_static_icons { get; set; }
            public Icon2 icon { get; set; }
        }

        public class Static_Icon2
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon2
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class _9
        {
            public Resized_Icons3[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon3 static_icon { get; set; }
            public Resized_Static_Icons3[] resized_static_icons { get; set; }
            public Icon3 icon { get; set; }
        }

        public class Static_Icon3
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon3
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons3
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons3
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class _5
        {
            public Resized_Icons4[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon4 static_icon { get; set; }
            public Resized_Static_Icons4[] resized_static_icons { get; set; }
            public Icon4 icon { get; set; }
        }

        public class Static_Icon4
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon4
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons4
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons4
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class _10
        {
            public Resized_Icons5[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon5 static_icon { get; set; }
            public Resized_Static_Icons5[] resized_static_icons { get; set; }
            public Icon5 icon { get; set; }
        }

        public class Static_Icon5
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon5
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons5
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons5
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class _25
        {
            public Resized_Icons6[] resized_icons { get; set; }
            public int awardings_required { get; set; }
            public Static_Icon6 static_icon { get; set; }
            public Resized_Static_Icons6[] resized_static_icons { get; set; }
            public Icon6 icon { get; set; }
        }

        public class Static_Icon6
        {
            public string url { get; set; }
            public int width { get; set; }
            public object format { get; set; }
            public int height { get; set; }
        }

        public class Icon6
        {
            public string url { get; set; }
            public int width { get; set; }
            public string format { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons6
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons6
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Icons7
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Resized_Static_Icons7
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

    }
}
