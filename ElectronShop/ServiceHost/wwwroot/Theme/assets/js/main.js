$(document).ready(function(){$(".dropdown-toggle").on("click",function(e){e.stopPropagation(),e.preventDefault();var a=$(this);if(a.is(".disabled, :disabled"))return!1;a.parent().toggleClass("open")}),$(document).on("click",function(e){$(".dropdown").hasClass("open")&&$(".dropdown").removeClass("open")}),$(".nav-btn").on("click",function(){$(".overlay").show(),$("nav").toggleClass("open")}),$(".overlay").on("click",function(){$("nav").hasClass("open")&&$("nav").removeClass("open"),$(this).hide()}),$("li.active").addClass("open").children("ul").show(),$("li.has-sub > a").on("click",function(){$(this).removeAttr("href");var e=$(this).parent("li");e.hasClass("open")?(e.removeClass("open"),e.find("li").removeClass("opne"),e.find("ul").slideUp(200)):(e.addClass("open"),e.children("ul").slideDown(200),e.siblings("li").children("ul").slideUp(200),e.siblings("li").removeClass("open"),e.siblings("li").find("li").removeClass("open"),e.siblings("li").find("ul").slideUp(200))}),$("li.nav-overlay").hover(function(){$(".sub-menu").removeClass("active"),$(".nav-categories-overlay").addClass("active")},function(){$(".nav-categories-overlay").removeClass("active")}),$(".search-box form input").on("click",function(){$(this).parents(".search-box").addClass("show-result").find(".search-result").fadeIn(0),$(".overlay-search-box").css({opacity:"1",visibility:"visible"})}),$(".search-box form input").keyup(function(){$(this).parents(".search-box").addClass("show-result").find(".search-result").fadeIn(0),$(this).parents(".search-box").find(".search-result-list").fadeIn(0),0==$(this).val().length?($(this).parents(".search-box").removeClass("show-result").find(".search-result").fadeOut(0),$(this).parents(".search-box").find(".search-result-list").fadeOut(0),$(this).parents(".search-box").removeClass("show-result")):($(this).parents(".search-box").find(".search-result").fadeIn(0),$(this).parents(".search-box").find(".search-result-list").fadeIn(0))}),$(document).click(function(e){$(e.target).is(".search-box *")||($(".search-result").hide(),$(".search-box").removeClass("show-result"),$(".overlay-search-box").css({opacity:"0",visibility:"hidden"}))}),$(window).scroll(function(){$(this).scrollTop()>60?$(".header-main-page").addClass("fixed"):$(".header-main-page").removeClass("fixed")});var e,a,i=0;window.addEventListener("scroll",function(){var e=window.pageYOffset||document.documentElement.scrollTop;e>i&&!$(".main-menu").hasClass("is-active")?($(".header-main-page .main-menu").addClass("hidden-nav-main"),$(".header-main-page .main-menu").slideUp(200)):($(".header-main-page .main-menu").removeClass("hidden-nav-main"),$(".header-main-page .main-menu").slideDown(200)),i=e}),$(".main-menu").on("mouseenter",function(){$(this).addClass("is-active")}),$(".main-menu").on("mouseleave",function(){$(this).removeClass("is-active")}),$(".item-list-menu>ul>li:first-child").addClass("active"),$(".item-list-menu>ul>li").on("mouseenter",function(){$(this).addClass("active").siblings().removeClass("active")}),e=jQuery,a={init:function(){a.countDown()},countDown:function(a,i){e(".countdown").each(function(){var a=e(this),n=e(this).data("date-time"),s=e(this).data("labels");(i||a).countdown(n,function(a){e(this).html(a.strftime('<div class="countdown-item"><div class="countdown-value">%D</div><div class="countdown-label">'+s["label-day"]+'</div></div><div class="countdown-item"><div class="countdown-value">%H</div><div class="countdown-label">'+s["label-hour"]+'</div></div><div class="countdown-item"><div class="countdown-value">%M</div><div class="countdown-label">'+s["label-minute"]+'</div></div><div class="countdown-item"><div class="countdown-value">%S</div><div class="countdown-label">'+s["label-second"]+"</div></div>"))})})}},e(function(){a.init()}),$(".sticky-sidebar").length&&$(".sticky-sidebar").theiaStickySidebar(),$(".custom-select-ui").length&&$(".custom-select-ui select").niceSelect(),$(".checkout-tab-pill").click(function(){var e=$(this).index();$(".checkout-tab-pill").removeClass("listing-active-cart"),$(this).addClass("listing-active-cart"),$(".cart-tab-main").slideUp(0),$(".cart-tab-main").eq(e).slideDown(0)}),$(".box-header-sidebar").on("click",function(e){e.preventDefault(),$(".box-header-sidebar").removeClass("activeacc"),$(this).addClass("activeacc"),$(this).next().slideToggle(200)}),$("li.box-tabs-tab").click(function(e){e.preventDefault();var a=$(this).index();$("li.box-tabs-tab").removeClass("active-tabs"),$(this).addClass("active-tabs"),$(".tab-active-content .tab").slideUp(0),$(".tab-active-content .tab").eq(a).slideDown(0)});var n=document.getElementById("modal");window.onclick=function(e){e.target==n&&(n.style.display="none")};var s=$("#advantage-input, #disadvantage-input"),t=function(){var e=$(this);e.val().trim().length>0?e.siblings(".js-icon-form-add").show():e.siblings(".js-icon-form-add").hide()};if(s.each(function(){t.bind(this)(),$(this).on("change keyup",t.bind(this))}),$("#advantages").delegate(".js-icon-form-add","click",function(e){var a=$(".js-advantages-list");if(!(a.find(".js-advantage-item").length>=5)){var i=$("#advantage-input");i.val().trim().length>0&&(a.append('<div class="ui-dynamic-label ui-dynamic-label--positive js-advantage-item">\n'+i.val()+'<button type="button" class="ui-dynamic-label-remove js-icon-form-remove"></button>\n<input type="hidden" name="comment[advantages][]" value="'+i.val()+'">\n</div>'),i.val("").change(),i.focus())}}).delegate(".js-icon-form-remove","click",function(e){$(this).parent(".js-advantage-item").remove()}),$("#disadvantages").delegate(".js-icon-form-add","click",function(e){var a=$(".js-disadvantages-list");if(!(a.find(".js-disadvantage-item").length>=5)){var i=$("#disadvantage-input");i.val().trim().length>0&&(a.append('<div class="ui-dynamic-label ui-dynamic-label--negative js-disadvantage-item">\n'+i.val()+'<button type="button" class="ui-dynamic-label-remove js-icon-form-remove"></button>\n<input type="hidden" name="comment[disadvantages][]" value="'+i.val()+'">\n</div>'),i.val("").change(),i.focus())}}).delegate(".js-icon-form-remove","click",function(e){$(this).parent(".js-disadvantage-item").remove()}),$(document).on("scroll",function(){var e=$(this).scrollTop();e>10?$(".footer-jump-angle").fadeIn(0,"swing"):e<300&&$(".footer-jump-angle").fadeOut(0,"swing")}),$(".footer-jump-angle").on("click",function(){$("html,body").animate({scrollTop:"0px"},3e3,"swing")}),$(document).scroll(function(){var e=$(document).scrollTop();e>200?$(".main-menu").addClass("NavFix"):e<10&&$(".main-menu").removeClass("NavFix")}),jQuery('<div class="quantity-nav"><div class="quantity-button quantity-up">+</div><div class="quantity-button quantity-down">-</div></div>').insertAfter(".quantity input"),jQuery(".quantity").each(function(){var e=jQuery(this),a=e.find('input[type="number"]'),i=e.find(".quantity-up"),n=e.find(".quantity-down"),s=a.attr("min"),t=a.attr("max");i.click(function(){var i=parseFloat(a.val());if(i>=t)var n=i;else n=i+1;e.find("input").val(n),e.find("input").trigger("change")}),n.click(function(){var i=parseFloat(a.val());if(i<=s)var n=i;else n=i-1;e.find("input").val(n),e.find("input").trigger("change")})}),$("#countdown-verify-end").length){var o=$("#countdown-verify-end");o.countdown({date:(new Date).getTime()+18e4,text:'<span class="day">%s</span><span class="hour">%s</span><span>: %s</span><span>%s</span>',end:function(){o.html("<a href='' class='link-border-verify form-account-link'>ارسال مجدد</a>")}})}function l(){$(".slide-progress").css({width:"100%",transition:"width 5000ms"})}$(".line-number-account").keyup(function(){$(this).next().focus()}),$("ul.gallery-options button.btn-option-wishes").on("click",function(){$(this).toggleClass("btn-option-favorites")}),$("#gallery-slider").owlCarousel({rtl:!0,margin:10,nav:!0,navText:['<i class="fa fa-angle-right"></i>','<i class="fa fa-angle-left"></i>'],dots:!1,responsiveClass:!0,responsive:{0:{items:4,slideBy:1}}}),$(".back-to-top").click(function(e){e.preventDefault(),$("html, body").animate({scrollTop:0},800,"easeInExpo")}),$("#img-product-zoom").length&&$("#img-product-zoom").ezPlus({zoomType:"inner",containLensZoom:!0,gallery:"gallery_01f",cursor:"crosshair",galleryActiveClass:"active",responsive:!0,imageCrossfade:!0,zoomWindowFadeIn:500,zoomWindowFadeOut:500}),$(".product-params-more-handler a").on("click",function(e){e.preventDefault(),$(".product-params-more").slideToggle(200),$(this).find(".show-more").fadeToggle(0),$(this).find(".show-less").fadeToggle(0)}),$(".table-suppliers-more a").on("click",function(e){e.preventDefault(),$(".in-list").slideToggle(200),$(this).find(".show-more").fadeToggle(0),$(this).find(".show-less").fadeToggle(0)}),$(".mask-handler").click(function(e){e.preventDefault();var a=$(this).parents(".content-expert-summary");a.find(".mask-text-product-summary").toggleClass("active"),a.find(".shadow-box").fadeToggle(0),$(this).find(".show-more").fadeToggle(0),$(this).find(".show-less").fadeToggle(0)}),$(".expert-article-button").click(function(e){e.preventDefault();var a=$(this).parents(".js-expert-article");a.find(".js-expert-article").toggleClass("active"),a.find(".content-expert-text").slideToggle(),$(this).find(".show-more").fadeToggle(0),$(this).find(".show-less").fadeToggle(0)}),$("#suggestion-slider").owlCarousel({rtl:!0,items:1,autoplay:!0,autoplayTimeout:5e3,loop:!0,dots:!1,onInitialized:l,onTranslate:function(){$(".slide-progress").css({width:0,transition:"width 0s"})},onTranslated:l}),$(".product-carousel").owlCarousel({rtl:!0,margin:10,nav:!0,navText:['<i class="fa fa-angle-right"></i>','<i class="fa fa-angle-left"></i>'],dots:!1,responsiveClass:!0,responsive:{0:{items:1,slideBy:1},576:{items:1,slideBy:1},768:{items:3,slideBy:2},992:{items:4,slideBy:2},1400:{items:4,slideBy:3}}})});